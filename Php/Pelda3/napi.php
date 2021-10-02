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
        <table align="center" border="1">
        <?php
        for($ora=8;$ora<=20;$ora++) {
            if( rand(0,1) == 1 )
                echo "<tr><td><input type='text' /></td></tr>";
            else 
                echo "<tr><td><input type='text' value='Ez jött az adatbázisból'/></td></tr>";
        }
        ?>
        </table>
        <button>Mentés</button>
    </body>
</html>
