const express = require('express');
const mysql = require('mysql');
const multer = require('multer');
const cors = require('cors');
const bodyParser = require('body-parser');
const bcrypt = require('bcrypt');
const jwt = require('jsonwebtoken');

// Middleware
const app = express();
app.use(cors());
app.use(express.json());
app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());

// Database connection
const db = mysql.createConnection({
  host: 'localhost',
  user: 'root',
  password: '',
  database: 'waterbilling',
});

db.connect((err) => {
  if (err) throw err;
  console.log('Connected to MySQL database');
});

// Generate a random secret key
//const SECRET_KEY = crypto.randomBytes(32).toString('hex');
const SECRET_KEY ='angelo10192001';
// Token verification middleware
const verifyToken = (req, res, next) => {
  const token = req.headers.authorization;
  
  if (!token) {
    return res.status(401).send({ error: 'No token provided' });
  }

  jwt.verify(token, SECRET_KEY, (err, decoded) => {
    if (err) {
      return res.status(401).send({ error: 'Invalid token' });
    }
    next();
  });
};

// Create account endpoint
app.post('/Create Account', (req, res) => {
  const { name, username, password } = req.query;

  if (!name || !username || !password) {
    res.status(400).send({ error: 'Missing parameter or role ID error' });
    return;
  }

  const token = jwt.sign({ username }, SECRET_KEY); // Generate JWT token

  bcrypt.hash(password, 10, (err, hashedPassword) => {
    if (err) {
      console.error('Error hashing password:', err);
      res.status(500).send({ error: 'Error creating account' });
      return;
    }

    const query =
      'INSERT INTO `account`(`Name`, `Username`, `Password`, `token`) VALUES (?,?,?,?)';

    db.query(query, [name, username, hashedPassword, token], (err, result) => {
      if (err) {
        console.error('Error creating account:', err);
        res.status(500).send({ error: 'Error creating account' });
      } else {
        res.status(200).send({ message: 'Account created successfully' });
      }
    });
  });
});
// Login endpoint
app.post('/login', (req, res) => {
  const { username, password } = req.query;
console.log(req.query);
  if (!username || !password) {
    res.status(400).send({ error: 'Missing username or password' });
    return;
  }

  const query =
    'SELECT * FROM account WHERE Username=?';
  db.query(query, [username], (err, result) => {
    if (err) {
      res.status(500).send({ error: 'Error fetching data' });
      return;
    }
    if (result.length === 0) {
      res.status(401).send({ error: 'Invalid credentials' });
      return;
    }
    const hashedPassword = result[0].Password;
    console.log(hashedPassword,' ',password);
    bcrypt.compare(password, hashedPassword, (err, passwordMatch) => {
      if (err) {
        res.status(500).send({ error: 'Error logging innnnnnnnnnn' });
        return;
      }

      if (passwordMatch) {
        res.status(200).send(result);
      } else {
        res.status(401).send({ error: 'Invalid password' });
      }
    });
  });
});

//Getting data endpoint
app.get('/waterbilling', verifyToken, (req, res) => {
  const displayData = req.query.displayData;
  if (!displayData.trim()  ) {
    res.status(400).send({ error: 'Invalid or missing Parameter' });
    return;
  } 
  let sqlQuery;
  if (displayData === "Billing") {
    sqlQuery = "SELECT `Client_ID`,concat( `First_Name`,' ', `Last_Name`) as Name, `Contact_Number`, `Purok` FROM client_info";
  } else if (displayData === "Client Information") {
    sqlQuery = "SELECT * FROM client_info";
  } else if (displayData === "Account") {
    sqlQuery = "SELECT * FROM account";
  } else if (displayData === "autoUpdate") {
    sqlQuery = "INSERT INTO `billing_info` (`Client_ID`, `Month`, `Year`, `Date`, `Status`) SELECT client_info.Client_ID, MONTHNAME(CURRENT_DATE), YEAR(CURRENT_DATE), CURRENT_DATE, 'Unpaid' FROM `client_info` WHERE( SELECT MAX(`Date`) FROM `billing_info` WHERE `billing_info`.`Client_ID` = `client_info`.`Client_ID` ) < CURRENT_DATE OR( SELECT MAX(`Date`) FROM `billing_info` WHERE `billing_info`.`Client_ID` = `client_info`.Client_ID) IS NULL;";
  }else {
    return res.status(400).send({ error: 'Invalid displayData value' });
  }
  db.query(sqlQuery, (err, result) => {
    if (err) {
      res.status(500).send({ error: 'Error fetching data' });
    } else {
      res.status(200).send(result);
    }
  });
  
});

//Searching data endpoint
app.get('/search', verifyToken, (req, res) => {
  let {displayData,searchData}=req.query;
  if (!displayData.trim()  || !searchData.trim() ) {
    res.status(400).send({ error: 'Invalid or missing Parameter' });
    return;
  }
  let sqlQuery;
  if (displayData === "Search Billing Information") {
    searchData=searchData+"%";
    sqlQuery ="SELECT `Client_ID`,concat( `First_Name`,' ', `Last_Name`) as Name, `Contact_Number`, `Purok` FROM client_info WHERE First_Name like ?";
  } 
  else if (displayData === "Paid History") {
    sqlQuery =  "SELECT `Billing_ID`, `Month`, `Year`,`Status` FROM `billing_info` WHERE Status = 'Paid' and Client_ID=?";
  } else if (displayData === "Search Client") {
    searchData=searchData+"%";
    sqlQuery = "SELECT * FROM client_info WHERE First_Name like ?";
  } 
  else if (displayData === "Unpaid Balance") {
    sqlQuery =  "SELECT `Billing_ID`, `Month`, `Year`,`Status` FROM `billing_info` WHERE Status='Unpaid' and Client_ID=?";
  } 
  else if (displayData === "Balance") {
    sqlQuery = "SELECT count(*) as Balance from  billing_info WHERE Status='Unpaid' and Client_ID=?";
  } 
  else {
    return res.status(400).send({ error: 'Invalid displayData value' });
  }
  db.query(sqlQuery,[searchData], (err, result) => {
    if (err) {
      res.status(500).send({ error: 'Error fetching data' });
    } else {
      res.status(200).send(result);
    }
  });
});

//Deleting data endpoint
app.delete('/waterbilling', verifyToken, (req, res) => {
  const {id,tableName}= req.query;
  console.log(id, tableName);
  let sqlQuery;
  if (!id  || isNaN(id) || !tableName.trim() ) {
    res.status(400).send({ error: 'Invalid or missing Parameter' });
    return;
  }
  if(tableName==="Billing Information"){
    sqlQuery = "DELETE FROM billing_info WHERE Client_ID = ?";
  }
  else if(tableName==="Client Information"){
    sqlQuery = "DELETE FROM client_info WHERE Client_ID = ?";
  }
  else if(tableName==="Account"){
    sqlQuery = "DELETE FROM account WHERE Account_ID = ?";
  }
  else {
    return res.status(400).send({ error: 'Invalid table name' });
  }
  db.query(sqlQuery, [id], (err, result) => {
    if (err) {
      res.status(500).send({ error: 'Error' });
    } else {
      res.status(200).send({ message: 'Successfully deleted' });
    }
  });
});

 //Updating data endpoint
app.put('/waterbilling', verifyToken, (req, res) => {
  const id=req.query.id;
  const tableName=req.query.tableName;

  if ( !id.trim()  || isNaN(id) || !tableName.trim() ) {
    res.status(400).send({ error: "Missing parameter or id error" });
    return;
  }

  if(tableName==="Client Information"){
    const {First_Name,Middle_Name,Last_Name,Contact_Number,Purok} = req.query;
    if (!First_Name.trim() || !Middle_Name.trim() || !Last_Name.trim() || !Contact_Number.trim() || !Purok.trim() ) {
      res.status(400).send({ error: "Missing parameter" });
      return;
    }
    const query = "UPDATE client_info SET First_Name = ?,Middle_Name = ?, Last_Name = ?, Contact_Number = ?,Purok = ? WHERE Client_ID = ?";

    db.query(query, [First_Name, Middle_Name,Last_Name,Contact_Number,Purok,id], (err, result) => {
      if (err) {
        res.status(500).send({ error: 'Error' });
      } else {
        res.status(200).send({ message: 'Successfully updated' });
      }
    });
  }
  else if(tableName==="Paid"){
    const query = "UPDATE `billing_info` SET `Status`='Paid' WHERE Billing_ID= ?";
    db.query(query, [id], (err, result) => {
      if (err) {
        res.status(500).send({ error: 'Error' });
      } else {
        res.status(200).send({ message: 'Successfully updated' });
      }
    });
  }
  else{
    return res.status(400).send({ error: 'Invalid table name' });
  }
});

//Adding data endpoint
app.post('/waterbilling', verifyToken, (req, res) => {
  const tableName=req.query.tableName;
  console.log(tableName);
  if(tableName==="Add Client"){
    const {First_Name,Middle_Name,Last_Name,Contact_Number,Purok} = req.query;
    console.log(req.query);
    if (!First_Name.trim() || !Middle_Name.trim() || !Last_Name.trim() || !Contact_Number.trim() || !Purok.trim()) {
      res.status(400).send({ error: "Missing parameter" });
      return;
    }
    const query = "INSERT INTO client_info (First_Name, Middle_Name, Last_Name, Contact_Number, Purok) VALUES (?,?,?,?,?)";
    ;
    db.query(query, [First_Name,Middle_Name,Last_Name,Contact_Number,Purok], (err, result) => {
      if (err) {
        res.status(500).send({ error: 'Error' });
      } else {
        res.status(200).send({ message: 'Successfully inserted' });
      }
    });
  }
else if(tableName==="Billing Balance"){
  const {Client_ID,Month,Year,Date,Status} = req.query;
  if (!Client_ID.trim() || !Month.trim() || !Year.trim()|| !Date.trim() || !Status.trim()) {
    res.status(400).send({ error: "Missing parameter or id error" });
    return;
  }
  const query = "INSERT INTO `billing_info` (`Client_ID`, `Month`, `Year`, `Date`, `Status`) SELECT client_info.Client_ID, MONTHNAME(CURRENT_DATE), YEAR(CURRENT_DATE), CURRENT_DATE, 'Unpaid' FROM `client_info` WHERE( SELECT MAX(`Date`) FROM `billing_info` WHERE `billing_info`.`Client_ID` = `client_info`.`Client_ID` ) < CURRENT_DATE OR( SELECT MAX(`Date`) FROM `billing_info` WHERE `billing_info`.`Client_ID` = `client_info`.Client_ID) IS NULL;";
  db.query(query, [Client_ID,Month,Year,Status], (err, result) => {
    if (err) {
      res.status(500).send({ error: 'Error' });
    } else {
      res.status(200).send({ message: 'Successfully inserted' });
    }
  });
}
else{
  return res.status(400).send({ error: 'Invalid table name' });
}
});

app.listen(3000, () => {
console.log('Server running on port 3000');
});
