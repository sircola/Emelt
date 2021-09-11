/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
/**
 * Author:  bernie
 * Created: 2021.09.10.
 */


DROP DATABASE IF EXISTS webapp1;

CREATE DATABASE webapp1 DEFAULT CHARACTER SET=utf8 COLLATE utf8_hungarian_ci;

USE webapp1;

CREATE TABLE felhasznalok (
    nev VARCHAR(1024),
    email VARCHAR(512) PRIMARY KEY,
    jelszo CHAR(40),
    telefon VARCHAR(512),
    statusz INT(1) DEFAULT 0
);

INSERT INTO felhasznalok(nev, email, telefon, jelszo, statusz) VALUES
('admin', 'admin@webapp', '', SHA1('admin'), 2);
