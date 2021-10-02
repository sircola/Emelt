-- phpMyAdmin SQL Dump
-- version 4.8.5
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2021. Feb 09. 17:21
-- Kiszolgáló verziója: 10.1.38-MariaDB
-- PHP verzió: 7.3.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `szendvicsbar`
--
CREATE DATABASE IF NOT EXISTS `szendvicsbar` DEFAULT CHARACTER SET utf8 COLLATE utf8_hungarian_ci;
USE `szendvicsbar`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `kinalat`
--

DROP TABLE IF EXISTS `kinalat`;
CREATE TABLE IF NOT EXISTS `kinalat` (
  `termekID` int(11) NOT NULL AUTO_INCREMENT,
  `ar` int(11) NOT NULL,
  `csomagolas` tinyint(1) NOT NULL,
  `nev` varchar(250) COLLATE utf8_hungarian_ci NOT NULL,
  `kep` varchar(50) COLLATE utf8_hungarian_ci NOT NULL,
  PRIMARY KEY (`termekID`),
  UNIQUE KEY `termekID` (`termekID`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- TÁBLA KAPCSOLATAI `kinalat`:
--

--
-- A tábla adatainak kiíratása `kinalat`
--

INSERT INTO `kinalat` (`termekID`, `ar`, `csomagolas`, `nev`, `kep`) VALUES
(1, 1000, 0, 'szendvics', 'szendvics.JPG'),
(2, 1500, 1, 'pizza', 'toll.JPG'),
(3, 900, 1, 'gyros', 'szendvics.JPG'),
(4, 800, 0, 'hamburger', 'nagyito.JPG'),
(8, 1200, 0, 'hotdog', 'pirosX.JPG'),
(9, 1100, 1, 'májgaluska leves', 'toll.JPG');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
