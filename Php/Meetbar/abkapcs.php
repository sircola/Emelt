<?php
define("HOST","localhost");
define("USER","root");
define("PASS","");
define("DB","szendvicsbar");

$kapcsolat=new mysqli(HOST,USER,PASS,DB) or die('Hiba az adatbázishoz csatlakozáskor');

$kapcsolat->query('SET NAMES UTF8');
$kapcsolat->query('SET CHARACTER SET UTF8');
$kapcsolat->query('SET COLLATION_CONNECTION="UTF8_HUNGARY_CI"');