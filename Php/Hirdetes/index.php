<?php
session_start();
if(!isset($_SESSION["loggedin"]) || $_SESSION["loggedin"] !== true) {
    
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
<title>Hirdetés</title>
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous">
<link href="formatumok.css" rel="stylesheet" type="text/css" />
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
<script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha256-/xUj+3OJU5yExlq6GSYGSHk7tPXikynS7ogEvDej/m4=" crossorigin="anonymous"></script>
<script src="funkciok.js"></script>
</head>
<body>
<div class="container">    
    <br>
    <h1 align="center">Hirdetések</h1>
    <hr>
    
    <br>
    <br>    

<?php

    $ab=new mysqli("localhost","root","","hirdetes");
    $ab->set_charset("utf8");

    $er=$ab->query("SELECT hirdetes.hid, hirdetes.uid, hirdetes.text, hirdetes.ar, felhasznalok.email, licitek.licit " .
                   "FROM hirdetes LEFT JOIN felhasznalok ON hirdetes.uid = felhasznalok.uid " .
                   "LEFT JOIN licitek ON licitek.hid = hirdetes.hid;");        
    
    while($sor=$er->fetch_assoc()){        

        echo '<div class="card shadow mx-2 my-2">';        

        echo '<div id="C3" class="card-header text-center">';
            echo $sor['email'];
        echo '</div>';                
              
        echo $sor['text'] . '<br/>';
        echo $sor['ar'] . ' Ft.';

        if(isset($_SESSION["loggedin"]) && $_SESSION["loggedin"]==true && $sor['uid'] != $_SESSION["uid"] ) {
        
            echo '<form action="licit.php" method="post">';
            echo '<input type="hidden" name="hid" value="' . $sor['hid'] . '">';
            echo 'Ár: <input id="ui9" name="ar" type="text" required value="'. strval(intval($sor['ar']) + 100) . '"/>';
            echo '<input name="elkuld" type="submit" value="Licitálok" />';
            echo '</form>';
        }
        
        echo '</div>';
    }
    
    $ab->close();
?>    
    
    <?php
    if(isset($_SESSION["loggedin"]) && $_SESSION["loggedin"]==true) {
        echo '<div style="text-align:center;height:60px">' .
             '<input type="button" onclick="location.href=' . 
             "'feltoltes.php'" .
             ';" value="Hirdetés feltöltése" />'.
             '</div>';
    }
    ?>
    
    
    <?php
    if(isset($_SESSION["loggedin"]) && $_SESSION["loggedin"]==true) {
        echo '<div style="text-align:center;height:60px">' .
             '<input type="button" onclick="location.href=' . 
             "'hirdeteseim.php'" .
             ';" value="Hirdetesek törlése" />'.
             '</div>';
    }
    ?>

    <?php
    if(isset($_POST["kilep"])){
        session_start();
        $_SESSION = array();
        session_destroy();
        echo "<script type='text/javascript'>window.location.href = 'index.php';</script>";
    }
   
    if(isset($_SESSION["loggedin"]) && $_SESSION["loggedin"]==true) {
    echo '<form method="post">'.
        '<table border="1" align="center" width="30%">'.
            '<tr>'.
                '<td colspan="2" style="text-align:left;"><h1>Kilépés:</h1></td>'.
            '</tr>'.
            '<tr>'.
                '<td colspan="2" style="text-align:left;">mint <b>'. $_SESSION["email"] .'</b>.</td>'.
            '</tr>'.
            '<tr>'.
                '<td id="gomb" colspan="2">'.
                    '<input name="kilep" type="submit" value="Kilépés" />'.
                '</td>'.
            '</tr>'.
        '</table>'.
    '</form>';
    }
    ?>    
    
    <?php
    if(isset($_POST["elkuld"])&&strlen($_POST["email"])>0&&strlen($_POST["jelszo"])>0){
        
        $ab=new mysqli("localhost","root","","hirdetes");
        $ab->set_charset("utf8");
        
        $email=htmlspecialchars($_POST['email']);
        $er=$ab->query("SELECT jelszo, uid FROM felhasznalok WHERE email='$email';");

        $ok=false;
        $sor=$er->fetch_row();
        if($sor&&$sor[0]==sha1($_POST["jelszo"])) {
            $_SESSION["loggedin"] = true;
            $_SESSION["email"] = $email;
            $_SESSION["uid"] = $sor[1];
            $ok = true;    
        }
        
        $ab->close();
          
        if($ok)
            echo "<script type='text/javascript'>window.location.href = 'index.php';</script>";
    }

    if(!isset($_SESSION["loggedin"]) || $_SESSION["loggedin"] !== true) {
    echo '<form method="post">'.
        '<table border="1" align="center" width="30%">'.
            '<tr>'.
                '<td colspan="2" style="text-align:left;"><h1>Belépés:</h1>vagy <a href="regisztracio.php">regisztráció.</a></td>'.
            '</tr>'.
            '<tr>'.
                '<td class="cim"><b>Email:</b></td>'.
                '<td><input id="ui1" name="email" type="text" placeholder="ko.pal@otthon.hu"/></td>'.
            '</tr>'.
            '<tr>'.
                '<td class="cim"><b>Jelszó:</b></td>'.
                '<td><input id="ui3" name="jelszo" type="password" /></td>'.
            '</tr>'.  
            '<tr>'.
                '<td id="gomb" colspan="2">'.
                    '<input name="elkuld" type="submit" value="Belépés" />'.
                '</td>'.
            '</tr>'.            
        '</table>'.
    '</form>';
    }
    ?>       
    
</div>    
</body>
</html>
