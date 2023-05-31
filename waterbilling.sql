-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 31, 2023 at 04:25 AM
-- Server version: 10.4.24-MariaDB
-- PHP Version: 8.1.6

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `waterbilling`
--

-- --------------------------------------------------------

--
-- Table structure for table `account`
--

CREATE TABLE `account` (
  `Account_ID` int(11) NOT NULL,
  `Username` varchar(50) NOT NULL,
  `Password` longtext NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Token` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `account`
--

INSERT INTO `account` (`Account_ID`, `Username`, `Password`, `Name`, `Token`) VALUES
(7, 'admin', '$2b$10$Iz4NVWWXWhbVYxlhgH046.gxkv5/sxiZKy8rpsx/k8P7wECERr5OK', 'Angelo', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImFkbWluIiwiaWF0IjoxNjg1NDM2NDI4fQ.AXk56WcS43TD08J_OAvP5UZYp8wVAs912P4HApEdmZI');

-- --------------------------------------------------------

--
-- Table structure for table `billing_info`
--

CREATE TABLE `billing_info` (
  `Billing_ID` int(11) NOT NULL,
  `Client_ID` int(11) NOT NULL,
  `Month` varchar(20) NOT NULL,
  `Year` year(4) NOT NULL,
  `Date` date NOT NULL,
  `Status` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `billing_info`
--

INSERT INTO `billing_info` (`Billing_ID`, `Client_ID`, `Month`, `Year`, `Date`, `Status`) VALUES
(695, 17, 'May', 2023, '2023-05-10', 'Paid'),
(696, 18, 'May', 2023, '2023-05-10', 'Paid'),
(697, 19, 'May', 2023, '2023-05-10', 'Paid'),
(698, 20, 'May', 2023, '2023-05-10', 'Paid'),
(699, 21, 'May', 2023, '2023-05-10', 'Unpaid'),
(701, 17, 'June', 2023, '2023-06-10', 'Paid'),
(702, 18, 'June', 2023, '2023-06-10', 'Paid'),
(703, 19, 'June', 2023, '2023-06-10', 'Unpaid'),
(704, 20, 'June', 2023, '2023-06-10', 'Paid'),
(705, 21, 'June', 2023, '2023-06-10', 'Unpaid'),
(708, 17, 'July', 2023, '2023-07-10', 'Unpaid'),
(709, 18, 'July', 2023, '2023-07-10', 'Unpaid'),
(710, 19, 'July', 2023, '2023-07-10', 'Paid'),
(711, 20, 'July', 2023, '2023-07-10', 'Unpaid'),
(712, 21, 'July', 2023, '2023-07-10', 'Unpaid'),
(715, 16, 'May', 2023, '2023-05-30', 'Unpaid');

-- --------------------------------------------------------

--
-- Table structure for table `client_info`
--

CREATE TABLE `client_info` (
  `Client_ID` int(11) NOT NULL,
  `Last_Name` varchar(50) NOT NULL,
  `First_Name` varchar(50) NOT NULL,
  `Middle_Name` varchar(50) NOT NULL,
  `Contact_Number` varchar(50) NOT NULL,
  `Purok` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `client_info`
--

INSERT INTO `client_info` (`Client_ID`, `Last_Name`, `First_Name`, `Middle_Name`, `Contact_Number`, `Purok`) VALUES
(16, 'Licmoan', 'Angelo', 'Bolocon', '09502699841', 2),
(17, 'Adebayo', 'Bam', 'Robinson', '0994283467', 69),
(18, 'Davis', 'Anthony', '', '9876546562', 3),
(19, 'Lang', 'Mark', 'Tahimik', '0912345678', 2),
(20, 'Pacquiao', 'Manny', '', '0987654321', 4),
(21, 'Butler', 'Jimmy', '', '974623486', 2);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `account`
--
ALTER TABLE `account`
  ADD PRIMARY KEY (`Account_ID`),
  ADD UNIQUE KEY `Username` (`Username`,`Token`) USING HASH;

--
-- Indexes for table `billing_info`
--
ALTER TABLE `billing_info`
  ADD PRIMARY KEY (`Billing_ID`),
  ADD KEY `fk_billing_info_client` (`Client_ID`);

--
-- Indexes for table `client_info`
--
ALTER TABLE `client_info`
  ADD PRIMARY KEY (`Client_ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `account`
--
ALTER TABLE `account`
  MODIFY `Account_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT for table `billing_info`
--
ALTER TABLE `billing_info`
  MODIFY `Billing_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=717;

--
-- AUTO_INCREMENT for table `client_info`
--
ALTER TABLE `client_info`
  MODIFY `Client_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=23;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `billing_info`
--
ALTER TABLE `billing_info`
  ADD CONSTRAINT `fk_billing_info_client` FOREIGN KEY (`Client_ID`) REFERENCES `client_info` (`Client_ID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
