<?php session_start(); 
?>

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
        <form action="" >
            user <input type=text" name="uid" />
            password <input type="password" name="pwd" />
            <input type="submit" name="kuldes" value="Bejelentkezés" />
        </form>
        <?php
            if(isset($_REQUEST["kuldes"])) {
                $ab=new mysqli("localhost","root","", "hirdeteses");
                $uid=$_REQUEST["uid"];
                $pwd=md5($_REQUEST["pwd"]);
                $er=$ab->query("SELECT FROM users WHERE uid=$uid AND pwd=$pwd LIMIT 1;");
                $sor=$er->fetch_row();
                if($sor[0]==$uid && $sor[1]==$pwd) {
                    $_SESSION["user"]=$uid;
                    header("Location: index.php");
                }
            }
        ?>
    </body>
</html>
