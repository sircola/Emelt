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
    <title>Feltöltés</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous">

    <link href="formatumok.css" rel="stylesheet" type="text/css" />
    
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha256-/xUj+3OJU5yExlq6GSYGSHk7tPXikynS7ogEvDej/m4=" crossorigin="anonymous"></script>

    <script src="funkciok.js"></script>
</head>
<body>
<div class="container">
        
   <?php
    if(isset($_POST["szoveg"])){
        
        $ab=new mysqli("localhost","root","","hirdetes");
        $ab->set_charset("utf8");
        
        $szoveg = htmlspecialchars($_POST["szoveg"]);
        $ar = $_POST["ar"];
        $uid = $_SESSION["uid"];
        
        var_dump($uid);
        
        $ab->query("INSERT INTO hirdetes(text,ar,uid) VALUES('$szoveg','$ar','$uid');");
        
        $aid = $ab->insert_id;
        
        $ab->close();
        
        echo "<script type='text/javascript'>window.location.href = 'index.php';</script>";
        exit;
    }
    ?>        
        
    <form method="post">
    <table id="uploadtable">
        <tr>
            <td colspan="3"><h1>Hirdetes feltöltése:</h1></td>
        </tr>
        <tr>
            <td class="cim"><b>Szöveg:</b></td>
            <td>
            <textarea id="ui4" name="szoveg" class="form-control my-2" rows="4" style="width:150%;margin-left:0%;" required></textarea>            
            </td>
        </tr>
        <tr>
            <td class="cim"><b>Ár:</b></td>
            <td><input id="ui9" name="ar" type="text" placeholder="pl. 150000" required /></td>
        </tr> 
        <tr>
            <td id="gomb">
                <input name="elkuld" type="submit" value="Feltöltés" />
                </td>
        </tr>            
    </table>
    </form>



</div>
</body>
</html>
