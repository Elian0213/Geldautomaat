-- --------------------------------------------------------
-- Host:                         192.168.0.162
-- Server version:               10.3.25-MariaDB-0ubuntu0.20.04.1 - Ubuntu 20.04
-- Server OS:                    debian-linux-gnu
-- HeidiSQL Version:             11.1.0.6116
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumping database structure for geldautomaat
CREATE DATABASE IF NOT EXISTS `geldautomaat` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;
USE `geldautomaat`;

-- Dumping structure for table geldautomaat.transactions
CREATE TABLE IF NOT EXISTS `transactions` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `amount` decimal(20,2) NOT NULL,
  `type` varchar(45) NOT NULL,
  `description` longtext DEFAULT NULL,
  `created_at` date NOT NULL DEFAULT current_timestamp(),
  `users_id` int(11) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_transactions_users` (`users_id`),
  CONSTRAINT `transactions_users` FOREIGN KEY (`users_id`) REFERENCES `users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- Dumping data for table geldautomaat.transactions: ~0 rows (approximately)
/*!40000 ALTER TABLE `transactions` DISABLE KEYS */;
INSERT INTO `transactions` (`id`, `amount`, `type`, `description`, `created_at`, `users_id`) VALUES
	(1, 300.00, 'deposit', NULL, '2020-12-27', 2),
	(2, 50.00, 'withdraw', NULL, '2020-12-27', 2),
	(3, 20.00, 'withdraw', NULL, '2020-12-27', 2);
/*!40000 ALTER TABLE `transactions` ENABLE KEYS */;

-- Dumping structure for table geldautomaat.users
CREATE TABLE IF NOT EXISTS `users` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `email` varchar(45) NOT NULL,
  `sex` varchar(45) NOT NULL,
  `birthday` date NOT NULL,
  `bsn_number` int(11) NOT NULL,
  `first_name` varchar(45) NOT NULL,
  `last_name` varchar(45) NOT NULL,
  `address` varchar(45) NOT NULL,
  `zipcode` varchar(45) NOT NULL,
  `town` varchar(45) NOT NULL,
  `pincode` varchar(64) NOT NULL,
  `account_number` int(11) NOT NULL,
  `balance` decimal(20,2) NOT NULL,
  `role` int(11) NOT NULL DEFAULT 0,
  `blocked` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- Dumping data for table geldautomaat.users: ~2 rows (approximately)
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` (`id`, `email`, `sex`, `birthday`, `bsn_number`, `first_name`, `last_name`, `address`, `zipcode`, `town`, `pincode`, `account_number`, `balance`, `role`, `blocked`) VALUES
	(1, 'elian@email.nl', 'male', '2002-02-13', 1234567, 'Elian', 'Achternaam', '9278 new road', '93027', 'kilcoole', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 1, 100.00, 1, 0),
	(2, 'gebruiker@email.nl', 'male', '2009-11-05', 44556677, 'Henk', 'Groot', 'Amsterdamstraat 12', '1234AB', 'Amsterdam', '78f9110cf588bd30afae93e571cd302097208933596793c8d07d9eaff8a2b456', 17073693, 230.00, 0, 0);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
