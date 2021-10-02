<?php session_start(); 
    // $_SESSION["user"]="bernat";
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
        <?php
            $user = isset($_SESSION["user"]);
            if(!$user) {
                echo "<h1>ALAP</h1>";
                echo "<a href='login.php'>Bejelentkezés</a>";
            }
            else {
                $user = $_SESSION["user"];
                echo "<h1>USER-es $user</h1>";
            }
                
        ?>
    </body>
</html>
