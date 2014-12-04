-- phpMyAdmin SQL Dump
-- version 4.0.10deb1
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Dec 04, 2014 at 08:08 PM
-- Server version: 5.5.40-0ubuntu0.14.04.1
-- PHP Version: 5.5.9-1ubuntu4.5

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `Test`
--

-- --------------------------------------------------------

--
-- Table structure for table `Component2DrugID`
--

CREATE TABLE IF NOT EXISTS `Component2DrugID` (
  `CID` varchar(20) CHARACTER SET armscii8 NOT NULL,
  `DID` varchar(20) CHARACTER SET armscii8 NOT NULL,
  `ComponentName` varchar(100) CHARACTER SET armscii8 NOT NULL,
  PRIMARY KEY (`CID`),
  KEY `DID` (`DID`,`ComponentName`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `Component2DrugID`
--

INSERT INTO `Component2DrugID` (`CID`, `DID`, `ComponentName`) VALUES
('C12300', 'DB00007', 'Terpinyl acetate'),
('C09187', 'DB00182', 'Evodiamine'),
('C02046', 'DB00424', 'Hyoscyamine'),
('C10307', 'DB01147', 'Cascaroside A'),
('C10230', 'DB01326', 'Geraniin'),
('C01399', 'DB02044', 'Agarose'),
('C06866', 'DB03241', 'Capsaicin'),
('C00180', 'DB03793', 'Benzoic acid'),
('C00757', 'DB04115', 'Berberine'),
('C00333', 'DB04213', 'D-Galacturonic acid'),
('C10453', 'DB09485', 'Eugenol'),
('C02284', 'DB09764', 'Glycyrrhizin'),
('C13194', 'DB12414', 'Calcium sulfate'),
('C09421', 'DB17349', 'Emetine'),
('C08589', 'DB20143', 'Crocin'),
('C17148', 'DB22235', 'Oleanolic acid'),
('C06522', 'DB33324', 'Strychnine'),
('C00561', 'DB33891', 'Mandelonitrile'),
('C10428', 'DB35812', 'Anethole'),
('C12626', 'DB44423', 'Trifolin'),
('C09800', 'DB55523', 'Swertiamarin'),
('C08978', 'DB77231', 'Senegin II'),
('C01479', 'DB80135', 'Atropine'),
('C08745', 'DB88723', 'Lithospermic acid');

-- --------------------------------------------------------

--
-- Table structure for table `Drug2Gene`
--

CREATE TABLE IF NOT EXISTS `Drug2Gene` (
  `DID` varchar(20) CHARACTER SET armscii8 NOT NULL,
  `GSymbol` varchar(20) CHARACTER SET armscii8 NOT NULL,
  PRIMARY KEY (`DID`,`GSymbol`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `Drug2Gene`
--

INSERT INTO `Drug2Gene` (`DID`, `GSymbol`) VALUES
('DB00007', 'A1BG'),
('DB00182', '42SP50'),
('DB00424', '130004C03'),
('DB01147', '18W'),
('DB01326', 'A2MP1'),
('DB02044', '128UP'),
('DB03241', 'A'),
('DB03793', '14-3-3ZETA'),
('DB04115', '1-SF'),
('DB04213', '7B2'),
('DB09485', 'A1CF'),
('DB09764', 'A3GALT2'),
('DB12414', '5430421B17'),
('DB17349', 'A1I3'),
('DB20143', '5-HT1B'),
('DB22235', 'AAAS'),
('DB28130', '5-HT7'),
('DB33324', 'A4GALT'),
('DB33891', 'A2ML1'),
('DB35812', 'A2M'),
('DB44423', 'AAA1'),
('DB55523', 'AAA2'),
('DB77231', 'A2ML'),
('DB80135', 'A2BP1'),
('DB88723', 'AACS');

-- --------------------------------------------------------

--
-- Table structure for table `Gene`
--

CREATE TABLE IF NOT EXISTS `Gene` (
  `GSymbol` varchar(50) CHARACTER SET armscii8 NOT NULL,
  `GeneName` varchar(100) CHARACTER SET armscii8 NOT NULL,
  `GBankId` varchar(50) CHARACTER SET armscii8 NOT NULL,
  `Uniport` varchar(50) CHARACTER SET armscii8 NOT NULL,
  PRIMARY KEY (`GSymbol`),
  KEY `GeneName` (`GeneName`,`GBankId`,`Uniport`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `Gene`
--

INSERT INTO `Gene` (`GSymbol`, `GeneName`, `GBankId`, `Uniport`) VALUES
('128UP', '128UP', 'L09210', 'P35228'),
('130004C03', '130004C03', '', ''),
('14-3-3ZETA', '14-3-3ZETA', '', ''),
('18W', '18W', '', '');

-- --------------------------------------------------------

--
-- Table structure for table `Gene2MeSH`
--

CREATE TABLE IF NOT EXISTS `Gene2MeSH` (
  `GSymbol` varchar(50) CHARACTER SET armscii8 NOT NULL,
  `MESHID` varchar(50) CHARACTER SET armscii8 NOT NULL,
  PRIMARY KEY (`GSymbol`,`MESHID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `Gene2MeSH`
--

INSERT INTO `Gene2MeSH` (`GSymbol`, `MESHID`) VALUES
('128UP', 'MESH:D003866'),
('130004C03', 'MESH:D000743'),
('14-3-3ZETA', 'MESH:D001008'),
('18W', 'MESH:D002277'),
('1-SF', 'MESH:D000014'),
('42SP50', 'MESH:D000152'),
('5430421B17', 'MESH:D058739'),
('5-HT1B', 'MESH:D000006'),
('5-HT7', 'MESH:D001008'),
('7B2', 'MESH:D006937'),
('A', 'MESH:C538231'),
('A1BG', 'MESH:D000008'),
('A1CF', 'MESH:D020434'),
('A1I3', 'MESH:D015746'),
('A2BP1', 'MESH:D000208'),
('A2M', 'MESH:D009008'),
('A2ML', 'MESH:C536875'),
('A2ML1', 'MESH:D000236'),
('A2MP1', 'MESH:D002375'),
('A2M-AS1', 'MESH:D015746'),
('A3GALT2', 'MESH:D002759'),
('A4GALT', 'MESH:D000230'),
('AAA1', 'MESH:D008175'),
('AAA2', 'OMIM:609782'),
('AAAS', 'MESH:D000037'),
('AACS', 'MESH:D020016');

-- --------------------------------------------------------

--
-- Table structure for table `Herb`
--

CREATE TABLE IF NOT EXISTS `Herb` (
  `Name` varchar(100) CHARACTER SET armscii8 NOT NULL DEFAULT '',
  `Tag` varchar(12) DEFAULT NULL,
  PRIMARY KEY (`Name`),
  KEY `Tag` (`Tag`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `Herb`
--

INSERT INTO `Herb` (`Name`, `Tag`) VALUES
('Apricot kernel water', 'JP16'),
('Belladonna root', 'JP16'),
('Cardamon', 'JP16'),
('Clove', 'JP16'),
('Condurango fluidextract', 'JP16'),
('Coptis rhizome', 'JP16'),
('Evodia fruit', 'JP16'),
('Forsythia fruit', 'JP16'),
('Geranium herb', 'JP16'),
('Glycyrrhiza', 'JP16'),
('Gypsum', 'JP16'),
('Lithospermum root', 'JP16'),
('Nux vomica', 'JP16'),
('Saffron', 'JP16'),
('Scopolia extract', 'JP16'),
('Senega', 'JP16'),
('Swertia herb', 'JP16'),
('Agar', 'JP16/NF'),
('Fennel oil', 'JP16/NF'),
('Tragacanth', 'JP16/NF'),
('Benzoin', 'JP16/USP'),
('Capsicum', 'JP16/USP'),
('Ipecac', 'JP16/USP'),
('Cascara sagrada', 'USP');

-- --------------------------------------------------------

--
-- Table structure for table `Herb2Comp`
--

CREATE TABLE IF NOT EXISTS `Herb2Comp` (
  `HerbName` varchar(120) CHARACTER SET armscii8 NOT NULL,
  `CID` varchar(100) CHARACTER SET armscii8 NOT NULL,
  PRIMARY KEY (`HerbName`,`CID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `Herb2Comp`
--

INSERT INTO `Herb2Comp` (`HerbName`, `CID`) VALUES
('Agar', 'C01399'),
('Apricot kernel water', 'C00561,C08325,C00712,C00249,C00422,C05005,C00865'),
('Belladonna root', 'C02046,C01479,C10862,C01851'),
('Benzoin', 'C00180,C10438,C00755,C17070,C17067,C00590'),
('Capsicum', 'C06866,C16952,C08584,C17515,C17516,C08889,C00072,C02094,C00719,C00114,C08601,C00473,C00378,C00255'),
('Cardamon', 'C12300,C09880,C09844,C03985,C09704,C16772,C17517,C17518,C06078'),
('Cascara sagrada', 'C10307,C16799,C16801,C16800'),
('Clove', 'C10453,C14567,C16930,C09629,C09684,C05442,C01753,C10224'),
('Condurango fluidextract', 'C12626,C10073,C01750,C05625,C08064,C00755,C00811,C00852,C17147,C01481,C05851,C09315,C09263,C09206,C1'),
('Coptis rhizome', 'C00757,C05315,C09553,C16938,C17083,C09581,C01494,C00852,C03329,C02890'),
('Evodia fruit', 'C09187,C09238,C16956,C17511,C17512,C04548,C00942,C03514,C09873,C08779'),
('Fennel oil', 'C10428,C10452,C09859,C06078,C09880,C09882,C06076,C09900,C06575,C00808,C10761'),
('Forsythia fruit', 'C17148,C10545,C16915,C08619,C08988,C10872,C17048,C17527,C17528,C10456,C10501,C10499,C05625,C01750,C1'),
('Geranium herb', 'C10230,C00389,C16981,C05903,C01424,C00042,C00230,C01108,C10788'),
('Glycyrrhiza', 'C02284,C17764,C16989,C0976,C16978,C0865,C16986,C16968,C00858,C17765,C06702\r\n,C02283'),
('Gypsum', 'C13194,C12567'),
('Ipecac', 'C09421,C09390,C09612,C17411,C09464,C09420,C11816'),
('Lithospermum root', 'C08745,C17771,C01551,C17412,C17413,C17414,C17415,C01850'),
('Nux vomica', 'C06522,C09084,C09255,C01433'),
('Saffron', 'C08589,C17055,C17062,C01789,C05442,C01753,C08988,C17148,C00249,C08362,C00712,C01595,C06427,C17513,C0'),
('Scopolia extract', 'C02046,C01479,C01851,C01752,C01527,C17921,C17922'),
('Senega', 'C08978,C17760,C17761,C12305'),
('Swertia herb', 'C09800,C17071,C09782,C09767,C17519,C10092,C10093,C10053,C10088,C10083,C10093,C17148,C17835,C10187,C0'),
('Tragacanth', 'C00333,C00369,C00760');

-- --------------------------------------------------------

--
-- Table structure for table `MESH2Disease`
--

CREATE TABLE IF NOT EXISTS `MESH2Disease` (
  `MESHID` varchar(20) CHARACTER SET armscii8 NOT NULL DEFAULT '',
  `DiseaseName` varchar(120) CHARACTER SET armscii8 DEFAULT NULL,
  PRIMARY KEY (`MESHID`),
  KEY `Disease Name` (`DiseaseName`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `MESH2Disease`
--

INSERT INTO `MESH2Disease` (`MESHID`, `DiseaseName`) VALUES
('MESH:D000006', 'Abdomen, Acute'),
('MESH:D000008', 'Abdominal Neoplasms'),
('MESH:D015746', 'Abdominal Pain'),
('MESH:D020434', 'Abducens Nerve Diseases'),
('MESH:D058739', 'Aberrant Crypt Foci'),
('MESH:D000014', 'Abnormalities, Drug-Induced'),
('MESH:D009008', 'Abnormalities, Severe Teratoid'),
('MESH:D000037', 'Abruptio Placentae'),
('MESH:D000152', 'Acne Vulgaris'),
('MESH:D020016', 'Activated Protein C Resistance'),
('MESH:D000208', 'Acute Disease'),
('MESH:D000230', 'Adenocarcinoma'),
('MESH:C538231', 'Adenocarcinoma of lung'),
('MESH:D000236', 'Adenoma'),
('MESH:D002759', 'Adenoma, Bile Duct'),
('MESH:D000743', 'Anemia, Hemolytic'),
('MESH:D001008', 'Anxiety Disorders'),
('OMIM:609782', 'AORTIC ANEURYSM'),
('MESH:C536875', 'Arrest of spermatogenesis'),
('MESH:D002277', 'Carcinoma'),
('MESH:D002375', 'Catalepsy'),
('MESH:D003866', 'Depressive Disorder'),
('MESH:D006937', 'Hypercholesterolemia'),
('MESH:D008175', 'Lung Neoplasms');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
