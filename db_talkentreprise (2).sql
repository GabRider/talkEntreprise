-- phpMyAdmin SQL Dump
-- version 4.1.4
-- http://www.phpmyadmin.net
--
-- Client :  127.0.0.1
-- Généré le :  Mer 08 Juin 2016 à 16:40
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
  `comment` varchar(70) NOT NULL,
  PRIMARY KEY (`idCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

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
(8, '0003', 'Envoyer un message'),
(9, '0014', 'Date ancien message'),
(10, '!!!!', 'début de la trame'),
(11, '0015', 'Mise à jour de la liste des employés'),
(12, '0004', 'récupérer les informations de l''utilsateur'),
(13, '0003', 'récupération des messages depuis la base de données'),
(14, '005', 'mise à jour des utilisateurs'),
(15, '', '');

-- --------------------------------------------------------

--
-- Structure de la table `t_group`
--

CREATE TABLE IF NOT EXISTS `t_group` (
  `idGroup` int(11) NOT NULL AUTO_INCREMENT,
  `group` varchar(20) NOT NULL,
  PRIMARY KEY (`idGroup`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

--
-- Contenu de la table `t_group`
--

INSERT INTO `t_group` (`idGroup`, `group`) VALUES
(1, 'vendeur'),
(2, 'R.H.');

-- --------------------------------------------------------

--
-- Structure de la table `t_log`
--

CREATE TABLE IF NOT EXISTS `t_log` (
  `idLog` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(4) NOT NULL,
  `lenTot` varchar(4) NOT NULL,
  `CodeSender` varchar(4) NOT NULL,
  `lenSender` varchar(4) NOT NULL,
  `valueSender` varchar(100) NOT NULL,
  `codeDestination` varchar(4) NOT NULL,
  `lenDestination` varchar(4) NOT NULL,
  `valueDestination` varchar(100) NOT NULL,
  `codeMessage` varchar(4) NOT NULL,
  `lenMessage` varchar(4) NOT NULL,
  `valueMessage` text NOT NULL,
  `codeDate` varchar(4) NOT NULL,
  `lenDate` varchar(4) NOT NULL,
  `valueDate` datetime NOT NULL,
  `CodeEnd` varchar(4) NOT NULL,
  `state` int(11) NOT NULL DEFAULT '2',
  `forGroup` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`idLog`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=759 ;

--
-- Contenu de la table `t_log`
--

INSERT INTO `t_log` (`idLog`, `code`, `lenTot`, `CodeSender`, `lenSender`, `valueSender`, `codeDestination`, `lenDestination`, `valueDestination`, `codeMessage`, `lenMessage`, `valueMessage`, `codeDate`, `lenDate`, `valueDate`, `CodeEnd`, `state`, `forGroup`) VALUES
(700, '0002', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-08 11:06:58', '####', 2, 0),
(701, '0001', '003F', '0011', '0018', 'poutcheck@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-08 11:07:55', '####', 2, 0),
(702, '0003', '0075', '0011', '0018', 'poutcheck@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001C', '210,4,219,187,217,45,104,130', '', '', '2016-06-08 11:08:04', '####', 3, 1),
(703, '0003', '0073', '0011', '0018', 'poutcheck@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001A', '92,92,105,113,239,58,22,15', '', '', '2016-06-08 11:08:05', '####', 3, 1),
(704, '0003', '0074', '0011', '0018', 'poutcheck@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001B', '0,64,186,139,220,54,230,130', '', '', '2016-06-08 11:08:07', '####', 3, 1),
(705, '0003', '0077', '0011', '0018', 'poutcheck@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001E', '172,229,249,37,162,145,131,166', '', '', '2016-06-08 11:08:52', '####', 3, 1),
(706, '0001', '003B', '0011', '0014', 'fabio@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-08 11:10:30', '####', 2, 0),
(707, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-08 11:11:07', '####', 2, 0),
(708, '0003', '0072', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0014', 'fabio@aspirateur.com', '0020', '001D', '174,197,91,205,230,185,12,106', '', '', '2016-06-08 11:11:14', '####', 3, 1),
(709, '0003', '0076', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001D', '174,197,91,205,230,185,12,106', '', '', '2016-06-08 11:11:14', '####', 3, 1),
(710, '0003', '0072', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0014', 'fabio@aspirateur.com', '0020', '001D', '174,197,91,205,230,185,12,106', '', '', '2016-06-08 11:11:20', '####', 3, 1),
(711, '0003', '0076', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001D', '174,197,91,205,230,185,12,106', '', '', '2016-06-08 11:11:20', '####', 3, 1),
(712, '0001', '003F', '0011', '0018', 'poutcheck@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-08 11:11:31', '####', 2, 0),
(713, '0003', '0074', '0011', '0018', 'poutcheck@aspirateur.com', '0012', '0014', 'fabio@aspirateur.com', '0020', '001D', '174,197,91,205,230,185,12,106', '', '', '2016-06-08 11:11:36', '####', 3, 1),
(714, '0003', '0076', '0011', '0018', 'poutcheck@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001D', '174,197,91,205,230,185,12,106', '', '', '2016-06-08 11:11:36', '####', 3, 1),
(715, '0003', '0072', '0011', '0018', 'poutcheck@aspirateur.com', '0012', '0014', 'fabio@aspirateur.com', '0020', '001B', '238,116,248,94,151,17,11,34', '', '', '2016-06-08 11:11:38', '####', 3, 1),
(716, '0003', '0074', '0011', '0018', 'poutcheck@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001B', '238,116,248,94,151,17,11,34', '', '', '2016-06-08 11:11:38', '####', 3, 1),
(717, '0003', '006F', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0014', 'fabio@aspirateur.com', '0020', '001A', '246,241,125,18,17,25,75,20', '', '', '2016-06-08 11:11:40', '####', 3, 1),
(718, '0003', '0073', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001A', '246,241,125,18,17,25,75,20', '', '', '2016-06-08 11:11:40', '####', 3, 1),
(719, '0003', '0072', '0011', '0018', 'poutcheck@aspirateur.com', '0012', '0014', 'fabio@aspirateur.com', '0020', '001B', '74,157,122,183,189,1,131,45', '', '', '2016-06-08 11:11:42', '####', 3, 1),
(720, '0003', '0074', '0011', '0018', 'poutcheck@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001B', '74,157,122,183,189,1,131,45', '', '', '2016-06-08 11:11:42', '####', 3, 1),
(721, '0003', '006F', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0014', 'fabio@aspirateur.com', '0020', '001A', '246,241,125,18,17,25,75,20', '', '', '2016-06-08 11:11:47', '####', 3, 1),
(722, '0003', '0073', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001A', '246,241,125,18,17,25,75,20', '', '', '2016-06-08 11:11:47', '####', 3, 1),
(723, '0003', '0072', '0011', '0018', 'poutcheck@aspirateur.com', '0012', '0014', 'fabio@aspirateur.com', '0020', '001B', '147,235,9,219,201,52,62,148', '', '', '2016-06-08 11:11:50', '####', 3, 1),
(724, '0003', '0074', '0011', '0018', 'poutcheck@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001B', '147,235,9,219,201,52,62,148', '', '', '2016-06-08 11:11:50', '####', 3, 1),
(725, '0003', '0070', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0014', 'fabio@aspirateur.com', '0020', '001B', '147,235,9,219,201,52,62,148', '', '', '2016-06-08 11:11:52', '####', 3, 1),
(726, '0003', '0074', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001B', '147,235,9,219,201,52,62,148', '', '', '2016-06-08 11:11:52', '####', 3, 1),
(727, '0002', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-08 11:11:54', '####', 2, 0),
(728, '0002', '003F', '0011', '0018', 'poutcheck@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-08 11:11:57', '####', 2, 0),
(729, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-08 14:27:20', '####', 2, 0),
(730, '0002', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-08 14:27:27', '####', 2, 0),
(731, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-08 14:27:38', '####', 2, 0),
(732, '0001', '003B', '0011', '0014', 'fabio@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-08 14:27:47', '####', 2, 0),
(733, '0003', '0071', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001C', '79,165,235,108,81,118,171,50', '', '', '2016-06-08 14:27:53', '####', 3, 1),
(734, '0003', '0073', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001C', '79,165,235,108,81,118,171,50', '', '', '2016-06-08 14:27:53', '####', 2, 1),
(735, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-08 14:28:37', '####', 2, 0),
(736, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-08 14:29:24', '####', 2, 0),
(737, '0001', '003B', '0011', '0014', 'fabio@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-08 14:29:31', '####', 2, 0),
(738, '0003', '0070', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001B', '114,193,9,215,163,135,203,1', '', '', '2016-06-08 14:29:39', '####', 3, 1),
(739, '0003', '0072', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001B', '114,193,9,215,163,135,203,1', '', '', '2016-06-08 14:29:39', '####', 2, 1),
(740, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-08 14:30:12', '####', 2, 0),
(741, '0003', '0070', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0014', 'fabio@aspirateur.com', '0020', '001B', '101,110,97,123,41,166,95,66', '', '', '2016-06-08 14:30:22', '####', 3, 1),
(742, '0003', '0074', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001B', '101,110,97,123,41,166,95,66', '', '', '2016-06-08 14:30:22', '####', 2, 1),
(743, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-08 14:31:14', '####', 2, 0),
(744, '0001', '003B', '0011', '0014', 'fabio@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-08 14:31:29', '####', 2, 0),
(745, '0003', '0072', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001D', '174,197,91,205,230,185,12,106', '', '', '2016-06-08 14:31:38', '####', 3, 1),
(746, '0003', '0074', '0011', '0014', 'fabio@aspirateur.com', '0012', '0018', 'poutcheck@aspirateur.com', '0020', '001D', '174,197,91,205,230,185,12,106', '', '', '2016-06-08 14:31:38', '####', 2, 1),
(747, '0001', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-08 14:32:17', '####', 2, 0),
(748, '0001', '003B', '0011', '0014', 'fabio@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-08 14:32:29', '####', 2, 0),
(749, '0003', '0070', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001B', '114,193,9,215,163,135,203,1', '', '', '2016-06-08 14:32:34', '####', 3, 0),
(750, '0003', '0071', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001C', '68,124,91,142,113,106,130,32', '', '', '2016-06-08 14:32:41', '####', 3, 0),
(751, '0003', '0072', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001D', '174,197,91,205,230,185,12,106', '', '', '2016-06-08 14:32:48', '####', 3, 0),
(752, '0003', '006F', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001A', '246,241,125,18,17,25,75,20', '', '', '2016-06-08 14:33:03', '####', 3, 0),
(753, '0003', '006F', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001A', '246,241,125,18,17,25,75,20', '', '', '2016-06-08 14:33:06', '####', 3, 0),
(754, '0003', '008F', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0014', 'fabio@aspirateur.com', '0020', '003A', '220,13,183,142,174,84,13,136,167,58,242,141,242,21,200,103', '', '', '2016-06-08 14:33:16', '####', 3, 0),
(755, '0003', '006F', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0014', 'fabio@aspirateur.com', '0020', '001A', '32,104,0,222,11,152,96,183', '', '', '2016-06-08 14:33:19', '####', 3, 0),
(756, '0003', '0072', '0011', '0014', 'fabio@aspirateur.com', '0012', '0016', 'gabriel@aspirateur.com', '0020', '001D', '174,197,91,205,230,185,12,106', '', '', '2016-06-08 14:33:36', '####', 3, 0),
(757, '0002', '003B', '0011', '0014', 'fabio@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-08 14:34:05', '####', 2, 0),
(758, '0002', '003D', '0011', '0016', 'gabriel@aspirateur.com', '0012', '0004', 'Host', '', '', '', '', '', '2016-06-08 14:34:06', '####', 2, 0);

-- --------------------------------------------------------

--
-- Structure de la table `t_state`
--

CREATE TABLE IF NOT EXISTS `t_state` (
  `idState` int(11) NOT NULL AUTO_INCREMENT,
  `state` varchar(70) NOT NULL,
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
  `idUser` varchar(60) NOT NULL,
  `password` varchar(50) NOT NULL,
  `connection` tinyint(1) NOT NULL DEFAULT '0',
  `idGroup` int(11) NOT NULL,
  PRIMARY KEY (`idUser`),
  UNIQUE KEY `idUser` (`idUser`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Contenu de la table `t_users`
--

INSERT INTO `t_users` (`idUser`, `password`, `connection`, `idGroup`) VALUES
('fabio@aspirateur.com', '641890219989519581101501581612559294203219190239', 0, 1),
('gabriel@aspirateur.com', '641890219989519581101501581612559294203219190239', 0, 1),
('poutcheck@aspirateur.com', '641890219989519581101501581612559294203219190239', 0, 1);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
