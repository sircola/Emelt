<!DOCTYPE html>
<!--
To change this license header, choose License Headers in Project Properties.
To change this template file, choose Tools | Templates
and open the template in the editor.
-->
<html>
<head>
    <meta charset="UTF-8">
    <title></title>
</head>
<body>
    <?php
    
    $nev = $_POST['nev'];
    $jelszo = $_POST['jelszo'];
    $jelszo_hash = sha1($jelszo);
    
    $ab=new mysqli("localhost","phpmyadmin","bernie","webapp1");
    $ab->set_charset("utf8");
        
    if($ab->connect_error)
        die("Hiba a csatlakozáskor.");
        
    $sql="SELECT nev FROM felhasznalok WHERE nev LIKE '$nev' AND jelszo LIKE '$jelszo_hash' AND statusz = 2;";

    $er=$ab->query($sql);
    
    if($er->num_rows==0){
        echo "not ok";
        header("Location: admin.php?err=login");
    }
    else {
        echo "ok";
        session_start();
        $_SESSION['user']=$nev;
        header("Location: admin.php");
    }

    $ab->close();
    ?>
</body>
</html>
