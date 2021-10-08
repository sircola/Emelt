<?php
session_start();
if(!isset($_SESSION["loggedin"]) || $_SESSION["loggedin"] !== true) {
    echo "<script type='text/javascript'>window.location.href = 'index.php';</script>";
}
?>

<!DOCTYPE html>
<!--
To change this license header, choose License Headers in Project Properties.
To change this template file, choose Tools | Templates
and open the template in the editor.
-->
<html lang="hu">
<head>
<meta charset="UTF-8">
<title>Hirdetéseim</title>
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous">
<link href="formatumok.css" rel="stylesheet" type="text/css" />
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
<script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha256-/xUj+3OJU5yExlq6GSYGSHk7tPXikynS7ogEvDej/m4=" crossorigin="anonymous"></script>
<script src="funkciok.js"></script>
</head>
<body>
<div class="container">    

    <br>
    
    <h1 align="center">Hirdetések törlése:</h1>
    <hr>    
    
    <?php
    

    $cnt=0;

    
    
    if($cnt<1) {
        echo '</br>'.
             "<h1 align='center'>Nincs hirdetese!</h1>".
             '<div style="text-align:center;height:60px">' .
             '<input type="button" onclick="location.href=' . 
             "'index.php'" .
             ';" value="Vissza" />'.
             '</div>';
    }
?>

</div>    
    
</body>
</html>
