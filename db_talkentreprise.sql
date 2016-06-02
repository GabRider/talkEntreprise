-- phpMyAdmin SQL Dump
-- version 4.1.4
-- http://www.phpmyadmin.net
--
-- Client :  127.0.0.1
-- Généré le :  Jeu 02 Juin 2016 à 13:22
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
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=16 ;

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
(11, '', ''),
(12, '', ''),
(13, '', ''),
(14, '', ''),
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
(1, 'Marketing'),
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
  `state` int(11) NOT NULL,
  `forGroup` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`idLog`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Structure de la table `t_state`
--

CREATE TABLE IF NOT EXISTS `t_state` (
  `idState` int(11) NOT NULL AUTO_INCREMENT,
  `state` varchar(70) NOT NULL,
  PRIMARY KEY (`idState`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

--
-- Contenu de la table `t_state`
--

INSERT INTO `t_state` (`idState`, `state`) VALUES
(1, 'Envoie en cours au serveur'),
(2, 'Utilisateur à vue le message');

-- --------------------------------------------------------

--
-- Structure de la table `t_users`
--

CREATE TABLE IF NOT EXISTS `t_users` (
  `idUser` varchar(60) NOT NULL,
  `password` varchar(50) NOT NULL,
  `connection` tinyint(1) NOT NULL DEFAULT '0',
  `idGroup` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
