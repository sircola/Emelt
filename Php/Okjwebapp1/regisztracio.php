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
    var_dump($_POST);
    
    $nev = $_POST['nev'];
    $email = $_POST['email'];
    $tel = $_POST['tel'];
    $jelszo = $_POST['jelszo'];
    $jelszo2 = $_POST['jelszo2'];

    if( $jelszo != $jelszo2 ) {
        header("Location: index.php?err=jelszo");
    }
    else {
        $jelszo_hash = sha1($jelszo);
        
        $ab=new mysqli("localhost","phpmyadmin","bernie","webapp1");
        $ab->set_charset("utf8");
        
        if($ab->connect_error)
            die("Hiba a csatlakozáskor.");
        
        $sql = "INSERT INTO felhasznalok(nev, email, telefon, jelszo) " .
                "VALUES('$nev','$email','$tel', '$jelszo_hash');";
        
        $er = $ab->query($sql);
        
        if($er)
            header("Location: index.php?reg=ok");
        else
            header("Location: index.php?reg=err");
                
        // $ab->close();
    }
    ?>
</body>
</html>
