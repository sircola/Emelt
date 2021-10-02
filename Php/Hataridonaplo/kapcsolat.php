<?php
define (HOST,'localhost');
define (FNEV,'root');
define (JELSZO,"");
define (ADATBAZIS, 'határidőnapló');

$kapcsolat=mysqli.connect(HOST, FNEV, JSZ, AB) OR die("HIBA");

mysqli_query("SET NAMES UTF8, $kapcsolat");

