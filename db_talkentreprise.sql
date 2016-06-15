-- phpMyAdmin SQL Dump
-- version 4.1.4
-- http://www.phpmyadmin.net
--
-- Client :  127.0.0.1
-- Généré le :  Mer 15 Juin 2016 à 13:22
-- Version du serveur :  5.6.15-log
-- Version de PHP :  5.4.24

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Base de données :  `db_talkentreprise`
--

-- --------------------------------------------------------

--
-- Structure de la table `t_code_help`
--

CREATE TABLE IF NOT EXISTS `t_code_help` (
  `idCode` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(6) NOT NULL,
  `comment` varchar(70) NOT NULL COMMENT 'explication des différents codes',
  PRIMARY KEY (`idCode`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=19 ;

--
-- Contenu de la table `t_code_help`
--

INSERT INTO `t_code_help` (`idCode`, `code`, `comment`) VALUES
(1, '0001', 'Connexion'),
(2, '0011', 'Envoyeur'),
(3, '0012', 'Destinataire'),
(4, '###', 'Fin'),
(5, '0002', 'Déconnection'),
(6, '0013', 'Heure'),
(7, '0020', 'Contenu du message'),
(8, '0003', 'Permet d''envoyer le message rédigé par un utilisateur au serveur pour '),
(9, '0014', 'Date ancien message'),
(11, '0015', 'Récupération des utilisateurs envoyés par le serveur'),
(12, '0004', 'Demande au serveur de lui donner les messages d''une conversation / réc'),
(14, '005', 'mise à jour des utilisateurs'),
(15, '0005', 'Envoi des informations de l''utilisateur au serveur'),
(16, '0006', 'Mise à jour de l''état des messages de la personne'),
(17, '0007', 'Demande au serveur de lui donner la liste des anciens messages / récup'),
(18, '0008', 'Demande au serveur d''enregistrer le nouveau mot de passe de l''utilisat');

-- --------------------------------------------------------

--
-- Structure de la table `t_group`
--

CREATE TABLE IF NOT EXISTS `t_group` (
  `idGroup` int(11) NOT NULL AUTO_INCREMENT,
  `group` varchar(20) NOT NULL COMMENT 'noms des différents secteurs présents dans l''entreprise',
  PRIMARY KEY (`idGroup`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=4 ;

--
-- Contenu de la table `t_group`
--

INSERT INTO `t_group` (`idGroup`, `group`) VALUES
(1, 'Vendeur'),
(2, 'R.H.'),
(3, 'Admin');

-- --------------------------------------------------------

--
-- Structure de la table `t_log`
--

CREATE TABLE IF NOT EXISTS `t_log` (
  `idLog` int(11) NOT NULL AUTO_INCREMENT COMMENT 'id unique d''une action faite par un utilisateur',
  `code` varchar(4) NOT NULL COMMENT 'code permettant d''identifier les différentes actions',
  `lenTot` varchar(4) NOT NULL COMMENT 'longueur total de la trame en hexadécimal',
  `CodeSender` varchar(4) NOT NULL COMMENT 'code permettant  d''identifier qui est l''envoyeur',
  `lenSender` varchar(4) NOT NULL COMMENT 'longueur total de l''identifiant de l''envoyeur  en hexadécimal',
  `valueSender` varchar(60) NOT NULL COMMENT 'identifiant de l''envoyeur',
  `codeDestination` varchar(4) NOT NULL COMMENT 'code permettant d''identifier qui est le receveur',
  `lenDestination` varchar(4) NOT NULL COMMENT 'longueur total de l''identifiant du destinataire  en hexadécimal',
  `valueDestination` varchar(60) NOT NULL COMMENT 'identifiant du receveur',
  `codeMessage` varchar(4) NOT NULL COMMENT 'code permettant d''identifier le message',
  `lenMessage` varchar(4) NOT NULL COMMENT 'longueur total de l''identifiant du message  en hexadécimal',
  `valueMessage` text NOT NULL COMMENT 'contenu du message (crypté)',
  `codeDate` varchar(4) NOT NULL COMMENT 'code permettant d''identifier la date',
  `lenDate` varchar(4) NOT NULL COMMENT 'longueur total de la date  en hexadécimal',
  `valueDate` datetime NOT NULL COMMENT 'valeur de la date et de l''heure',
  `CodeEnd` varchar(4) NOT NULL COMMENT 'fin de la tram',
  `state` int(11) NOT NULL DEFAULT '2' COMMENT 'état des messages',
  `forGroup` tinyint(1) NOT NULL DEFAULT '0' COMMENT 'si le message est destiné à un groupe',
  PRIMARY KEY (`idLog`),
  KEY `state` (`state`),
  KEY `valueSender` (`valueSender`),
  KEY `valueDestination` (`valueDestination`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2213 ;

--
-- Contenu de la table `t_log`
--

INSERT INTO `t_log` (`idLog`, `code`, `lenTot`, `CodeSender`, `lenSender`, `valueSender`, `codeDestination`, `lenDestination`, `valueDestination`, `codeMessage`, `lenMessage`, `valueMessage`, `codeDate`, `lenDate`, `valueDate`, `CodeEnd`, `state`, `forGroup`) VALUES
(2077, '0003', '0073', '0011', '0018', 'poutcheck@aspirateur.com', '0012', '0014', 'fabio@aspirateur.com', '0020', '001C', '68,124,91,142,113,106,130,32', '', '', '2016-06-13 11:02:50', '####', 4, 1),
(2078, '0003', '0075', '0011', '0018', 'poutcheck@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001C', '68,124,91,142,113,106,130,32', '', '', '2016-06-13 11:02:50', '####', 4, 1),
(2079, '0003', '0071', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0014', 'fabio@aspirateur.com', '0020', '001C', '68,124,91,142,113,106,130,32', '', '', '2016-06-13 11:03:03', '####', 4, 1),
(2080, '0003', '0075', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001C', '68,124,91,142,113,106,130,32', '', '', '2016-06-13 11:03:03', '####', 4, 1),
(2081, '0003', '008C', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '0037', '227,8,158,92,37,152,9,133,201,201,101,167,63,56,231,168', '', '', '2016-06-13 11:03:24', '####', 4, 1),
(2082, '0003', '008E', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '0037', '227,8,158,92,37,152,9,133,201,201,101,167,63,56,231,168', '', '', '2016-06-13 11:03:24', '####', 4, 1),
(2083, '0002', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-13 11:47:36', '####', 2, 0),
(2084, '0002', '003F', '0011', '0018', 'poutcheck@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-13 11:47:53', '####', 2, 0),
(2085, '0002', '003B', '0011', '0014', 'fabio@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-13 11:47:57', '####', 2, 0),
(2086, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-13 11:49:24', '####', 2, 0),
(2087, '0001', '003B', '0011', '0014', 'fabio@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-13 11:49:37', '####', 2, 0),
(2088, '0001', '003F', '0011', '0018', 'poutcheck@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-13 11:49:47', '####', 2, 0),
(2089, '0002', '003F', '0011', '0018', 'poutcheck@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-13 13:39:39', '####', 2, 0),
(2090, '0002', '003B', '0011', '0014', 'fabio@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-13 13:39:40', '####', 2, 0),
(2091, '0002', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-13 14:39:14', '####', 2, 0),
(2092, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 11:42:04', '####', 2, 0),
(2093, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 11:42:07', '####', 2, 0),
(2094, '0003', '006F', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0014', 'fabio@aspirateur.com', '0020', '001A', '254,68,250,245,2,4,135,219', '', '', '2016-06-14 11:42:24', '####', 4, 1),
(2095, '0003', '0073', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001A', '254,68,250,245,2,4,135,219', '', '', '2016-06-14 11:42:24', '####', 2, 1),
(2096, '0002', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 11:42:35', '####', 2, 0),
(2097, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 11:47:46', '####', 2, 0),
(2098, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 11:58:50', '####', 2, 0),
(2099, '0002', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 11:59:23', '####', 2, 0),
(2100, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 13:07:02', '####', 2, 0),
(2101, '0002', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 13:07:42', '####', 2, 0),
(2102, '0001', '003B', '0011', '0014', 'fabio@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 13:08:22', '####', 2, 0),
(2103, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 13:08:25', '####', 2, 0),
(2104, '0003', '0070', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001B', '147,235,9,219,201,52,62,148', '', '', '2016-06-14 13:09:04', '####', 4, 1),
(2105, '0003', '0072', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001B', '147,235,9,219,201,52,62,148', '', '', '2016-06-14 13:09:04', '####', 2, 1),
(2106, '0003', '0071', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001C', '43,10,230,130,195,245,193,12', '', '', '2016-06-14 13:09:07', '####', 4, 1),
(2107, '0003', '0073', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001C', '43,10,230,130,195,245,193,12', '', '', '2016-06-14 13:09:07', '####', 2, 1),
(2108, '0003', '0071', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001C', '169,192,206,142,3,43,216,128', '', '', '2016-06-14 13:09:09', '####', 4, 1),
(2109, '0003', '0073', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001C', '169,192,206,142,3,43,216,128', '', '', '2016-06-14 13:09:09', '####', 2, 1),
(2110, '0003', '006D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0014', 'fabio@aspirateur.com', '0020', '0018', '204,143,56,236,71,0,6,73', '', '', '2016-06-14 13:09:17', '####', 4, 1),
(2111, '0003', '0071', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '0018', '204,143,56,236,71,0,6,73', '', '', '2016-06-14 13:09:18', '####', 2, 1),
(2112, '0003', '006F', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0014', 'fabio@aspirateur.com', '0020', '001A', '1,191,87,211,82,226,227,75', '', '', '2016-06-14 13:09:21', '####', 4, 1),
(2113, '0003', '0073', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001A', '1,191,87,211,82,226,227,75', '', '', '2016-06-14 13:09:21', '####', 2, 1),
(2114, '0003', '008E', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0014', 'fabio@aspirateur.com', '0020', '0039', '233,199,239,192,140,44,231,27,30,52,58,131,87,148,202,241', '', '', '2016-06-14 13:09:28', '####', 4, 1),
(2115, '0003', '0092', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '0039', '233,199,239,192,140,44,231,27,30,52,58,131,87,148,202,241', '', '', '2016-06-14 13:09:28', '####', 2, 1),
(2116, '0003', '0071', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001C', '179,218,171,89,203,65,213,19', '', '', '2016-06-14 13:09:38', '####', 4, 1),
(2117, '0003', '0073', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001C', '179,218,171,89,203,65,213,19', '', '', '2016-06-14 13:09:38', '####', 2, 1),
(2118, '0003', '0071', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001C', '201,58,63,168,16,185,155,135', '', '', '2016-06-14 13:09:39', '####', 4, 1),
(2119, '0003', '0073', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001C', '201,58,63,168,16,185,155,135', '', '', '2016-06-14 13:09:40', '####', 2, 1),
(2120, '0003', '0071', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001C', '201,58,63,168,16,185,155,135', '', '', '2016-06-14 13:09:44', '####', 4, 1),
(2121, '0003', '0073', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001C', '201,58,63,168,16,185,155,135', '', '', '2016-06-14 13:09:44', '####', 2, 1),
(2122, '0003', '0071', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001C', '186,206,163,48,151,29,83,155', '', '', '2016-06-14 13:09:46', '####', 4, 1),
(2123, '0003', '0073', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001C', '186,206,163,48,151,29,83,155', '', '', '2016-06-14 13:09:46', '####', 2, 1),
(2124, '0003', '0074', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001F', '177,225,228,114,145,237,113,184', '', '', '2016-06-14 13:09:47', '####', 4, 1),
(2125, '0003', '0076', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001F', '177,225,228,114,145,237,113,184', '', '', '2016-06-14 13:09:47', '####', 2, 1),
(2126, '0002', '003B', '0011', '0014', 'fabio@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 13:09:52', '####', 2, 0),
(2127, '0002', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 13:09:56', '####', 2, 0),
(2128, '0001', '003A', '0011', '0013', 'juan@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 13:26:54', '####', 2, 0),
(2129, '0001', '003A', '0011', '0013', 'juan@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 13:41:39', '####', 2, 0),
(2130, '0002', '003A', '0011', '0013', 'juan@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 13:41:44', '####', 2, 0),
(2131, '0001', '003A', '0011', '0013', 'juan@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 13:46:10', '####', 2, 0),
(2132, '0002', '003A', '0011', '0013', 'juan@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 13:46:12', '####', 2, 0),
(2133, '0001', '003A', '0011', '0013', 'juan@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 13:46:27', '####', 2, 0),
(2134, '0001', '003A', '0011', '0013', 'juan@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 13:47:02', '####', 2, 0),
(2135, '0002', '003A', '0011', '0013', 'juan@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 13:47:08', '####', 2, 0),
(2136, '0001', '003A', '0011', '0013', 'juan@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 13:47:15', '####', 2, 0),
(2137, '0002', '003A', '0011', '0013', 'juan@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 13:47:19', '####', 2, 0),
(2138, '0001', '003A', '0011', '0013', 'juan@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 13:48:07', '####', 2, 0),
(2139, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 13:48:24', '####', 2, 0),
(2140, '0002', '003A', '0011', '0013', 'juan@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 13:48:29', '####', 2, 0),
(2141, '0002', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 13:48:54', '####', 2, 0),
(2142, '0001', '003A', '0011', '0013', 'juan@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 14:01:13', '####', 2, 0),
(2143, '0002', '003A', '0011', '0013', 'juan@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 14:03:11', '####', 2, 0),
(2144, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 14:26:52', '####', 2, 0),
(2145, '0002', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 14:26:59', '####', 2, 0),
(2146, '0001', '003B', '0011', '0014', 'fabio@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 14:28:15', '####', 2, 0),
(2147, '0003', '0074', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001F', '177,225,228,114,145,237,113,184', '', '', '2016-06-14 14:28:25', '####', 4, 1),
(2148, '0003', '0076', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001F', '177,225,228,114,145,237,113,184', '', '', '2016-06-14 14:28:25', '####', 2, 1),
(2149, '0003', '0071', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001C', '107,245,196,25,142,248,21,22', '', '', '2016-06-14 14:28:34', '####', 4, 1),
(2150, '0003', '0073', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001C', '107,245,196,25,142,248,21,22', '', '', '2016-06-14 14:28:34', '####', 2, 1),
(2151, '0003', '0071', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001C', '37,26,200,187,133,38,222,254', '', '', '2016-06-14 14:28:50', '####', 4, 1),
(2152, '0003', '0073', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001C', '37,26,200,187,133,38,222,254', '', '', '2016-06-14 14:28:51', '####', 2, 1),
(2153, '0003', '0070', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001B', '22,169,25,197,47,81,111,213', '', '', '2016-06-14 14:28:52', '####', 4, 1),
(2154, '0003', '0072', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001B', '22,169,25,197,47,81,111,213', '', '', '2016-06-14 14:28:52', '####', 2, 1),
(2155, '0002', '003B', '0011', '0014', 'fabio@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 14:28:55', '####', 2, 0),
(2156, '0001', '003B', '0011', '0014', 'fabio@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 14:28:58', '####', 2, 0),
(2157, '0003', '006D', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '0018', '106,246,22,214,43,8,7,49', '', '', '2016-06-14 14:29:03', '####', 4, 1),
(2158, '0003', '006F', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '0018', '106,246,22,214,43,8,7,49', '', '', '2016-06-14 14:29:03', '####', 2, 1),
(2159, '0003', '0072', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001D', '172,52,191,73,224,213,102,159', '', '', '2016-06-14 14:29:05', '####', 4, 1),
(2160, '0003', '0074', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001D', '172,52,191,73,224,213,102,159', '', '', '2016-06-14 14:29:05', '####', 2, 1),
(2161, '0003', '0070', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001B', '64,202,244,139,231,33,218,9', '', '', '2016-06-14 14:29:06', '####', 4, 1),
(2162, '0003', '0072', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001B', '64,202,244,139,231,33,218,9', '', '', '2016-06-14 14:29:06', '####', 2, 1),
(2163, '0003', '006F', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001A', '167,53,188,90,50,17,65,161', '', '', '2016-06-14 14:29:08', '####', 4, 1),
(2164, '0003', '0071', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001A', '167,53,188,90,50,17,65,161', '', '', '2016-06-14 14:29:08', '####', 2, 1),
(2165, '0003', '006F', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001A', '37,141,113,59,232,29,1,196', '', '', '2016-06-14 14:29:31', '####', 4, 0),
(2166, '0003', '0072', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001D', '163,161,41,248,199,116,120,20', '', '', '2016-06-14 14:29:35', '####', 4, 1),
(2167, '0003', '0074', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001D', '163,161,41,248,199,116,120,20', '', '', '2016-06-14 14:29:35', '####', 2, 1),
(2168, '0003', '0070', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001B', '74,218,56,13,151,46,221,129', '', '', '2016-06-14 14:29:46', '####', 4, 1),
(2169, '0003', '0072', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001B', '74,218,56,13,151,46,221,129', '', '', '2016-06-14 14:29:46', '####', 2, 1),
(2170, '0003', '0071', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001C', '182,243,151,245,76,105,107,2', '', '', '2016-06-14 14:29:49', '####', 4, 1),
(2171, '0003', '0073', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001C', '182,243,151,245,76,105,107,2', '', '', '2016-06-14 14:29:49', '####', 2, 1),
(2172, '0003', '0070', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001B', '84,57,148,61,11,240,181,107', '', '', '2016-06-14 14:29:50', '####', 4, 1),
(2173, '0003', '0072', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001B', '84,57,148,61,11,240,181,107', '', '', '2016-06-14 14:29:50', '####', 2, 1),
(2174, '0003', '0071', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001C', '50,78,186,93,105,201,233,100', '', '', '2016-06-14 14:29:59', '####', 4, 1),
(2175, '0003', '0073', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001C', '50,78,186,93,105,201,233,100', '', '', '2016-06-14 14:29:59', '####', 2, 1),
(2176, '0003', '006F', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001A', '254,68,250,245,2,4,135,219', '', '', '2016-06-14 14:30:10', '####', 4, 1),
(2177, '0003', '0071', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001A', '254,68,250,245,2,4,135,219', '', '', '2016-06-14 14:30:10', '####', 2, 1),
(2178, '0002', '003B', '0011', '0014', 'fabio@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 14:30:15', '####', 2, 0),
(2179, '0001', '003B', '0011', '0014', 'fabio@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 14:31:23', '####', 2, 0),
(2180, '0003', '0070', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001B', '222,79,61,36,227,223,238,29', '', '', '2016-06-14 14:31:28', '####', 4, 1),
(2181, '0003', '0072', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001B', '222,79,61,36,227,223,238,29', '', '', '2016-06-14 14:31:28', '####', 2, 1),
(2182, '0003', '006F', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001A', '35,177,24,109,156,42,13,42', '', '', '2016-06-14 14:31:30', '####', 4, 1),
(2183, '0003', '0071', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001A', '35,177,24,109,156,42,13,42', '', '', '2016-06-14 14:31:30', '####', 2, 1),
(2184, '0003', '0071', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001C', '212,66,141,112,134,68,68,187', '', '', '2016-06-14 14:31:42', '####', 4, 1),
(2185, '0003', '0073', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001C', '212,66,141,112,134,68,68,187', '', '', '2016-06-14 14:31:42', '####', 2, 1),
(2186, '0003', '0070', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001B', '18,19,174,137,52,111,219,33', '', '', '2016-06-14 14:31:46', '####', 4, 0),
(2187, '0003', '006F', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001A', '75,18,77,45,189,81,136,181', '', '', '2016-06-14 14:31:48', '####', 4, 1),
(2188, '0003', '0071', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001A', '75,18,77,45,189,81,136,181', '', '', '2016-06-14 14:31:48', '####', 2, 1),
(2189, '0001', '003A', '0011', '0013', 'juan@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 14:32:14', '####', 2, 0),
(2190, '0002', '003A', '0011', '0013', 'juan@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 14:32:25', '####', 2, 0),
(2191, '0001', '003A', '0011', '0013', 'juan@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 14:32:26', '####', 2, 0),
(2192, '0002', '003A', '0011', '0013', 'juan@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-14 14:32:43', '####', 2, 0),
(2193, '0001', '003A', '0011', '0013', 'juan@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-15 06:13:32', '####', 2, 0),
(2194, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-15 06:14:17', '####', 2, 0),
(2195, '0003', '0071', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0014', 'fabio@aspirateur.com', '0020', '001C', '119,51,149,179,44,88,101,163', '', '', '2016-06-15 06:14:47', '####', 2, 0),
(2196, '0002', '003A', '0011', '0013', 'juan@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-15 06:15:26', '####', 2, 0),
(2197, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-15 06:15:49', '####', 2, 0),
(2198, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-15 06:28:04', '####', 2, 0),
(2199, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-15 06:29:33', '####', 2, 0),
(2200, '0003', '0072', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0014', 'fabio@aspirateur.com', '0020', '001D', '108,15,104,171,185,48,191,232', '', '', '2016-06-15 06:29:44', '####', 2, 1),
(2201, '0003', '0076', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001D', '108,15,104,171,185,48,191,232', '', '', '2016-06-15 06:29:44', '####', 2, 1),
(2202, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-15 06:34:04', '####', 2, 0),
(2203, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-15 06:34:36', '####', 2, 0),
(2204, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-15 06:36:07', '####', 2, 0),
(2205, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-15 06:37:02', '####', 2, 0),
(2206, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-15 06:38:45', '####', 2, 0),
(2207, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-15 06:40:22', '####', 2, 0),
(2208, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-15 06:48:20', '####', 2, 0),
(2209, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-15 06:49:16', '####', 2, 0),
(2210, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-15 06:49:50', '####', 2, 0),
(2211, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-15 06:52:37', '####', 2, 0),
(2212, '0002', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-15 06:53:22', '####', 2, 0);

-- --------------------------------------------------------

--
-- Structure de la table `t_state`
--

CREATE TABLE IF NOT EXISTS `t_state` (
  `idState` int(11) NOT NULL AUTO_INCREMENT,
  `state` varchar(70) NOT NULL COMMENT 'différents états d''un message',
  PRIMARY KEY (`idState`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=5 ;

--
-- Contenu de la table `t_state`
--

INSERT INTO `t_state` (`idState`, `state`) VALUES
(1, 'Envoie en cours au serveur'),
(2, 'Serveur reçu'),
(3, 'Utilisateur connecter mais n''as pas vu le message'),
(4, 'Utilisateur à vue le message');

-- --------------------------------------------------------

--
-- Structure de la table `t_users`
--

CREATE TABLE IF NOT EXISTS `t_users` (
  `idUser` varchar(60) NOT NULL COMMENT 'identifiant d''un utilisateur',
  `password` varchar(255) NOT NULL COMMENT 'mot de passe de l''utilisateur',
  `connection` tinyint(1) NOT NULL DEFAULT '0' COMMENT 'état de connexion de l''utilisateur',
  `idGroup` int(11) NOT NULL COMMENT 'identifiant du groupe auquel l''utilisateur appartient',
  PRIMARY KEY (`idUser`),
  UNIQUE KEY `idUser` (`idUser`),
  KEY `idGroup` (`idGroup`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Contenu de la table `t_users`
--

INSERT INTO `t_users` (`idUser`, `password`, `connection`, `idGroup`) VALUES
('fabio@aspirateur.com', '124741419202559817597229149321486119410014824814827', 0, 1),
('gabriel@aspirateur.com', '124741419202559817597229149321486119410014824814827', 0, 1),
('juan@aspirateur.com', '124741419202559817597229149321486119410014824814827', 0, 3),
('pierre@aspirateur.com', '124741419202559817597229149321486119410014824814827', 0, 3),
('poutcheck@aspirateur.com', '124741419202559817597229149321486119410014824814827', 0, 1);

--
-- Contraintes pour les tables exportées
--

--
-- Contraintes pour la table `t_log`
--
ALTER TABLE `t_log`
  ADD CONSTRAINT `t_log_ibfk_1` FOREIGN KEY (`state`) REFERENCES `t_state` (`idState`),
  ADD CONSTRAINT `t_log_ibfk_2` FOREIGN KEY (`valueSender`) REFERENCES `t_users` (`idUser`);

--
-- Contraintes pour la table `t_users`
--
ALTER TABLE `t_users`
  ADD CONSTRAINT `t_users_ibfk_1` FOREIGN KEY (`idGroup`) REFERENCES `t_group` (`idGroup`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
