<!DOCTYPE html>
<!--
To change this license header, choose License Headers in Project Properties.
To change this template file, choose Tools | Templates
and open the template in the editor.
-->
<html>
<head>
    <meta charset="UTF-8">
    <link href="formatumok.css" rel="stylesheet" />
    <title>Regisztrációs oldal</title>
</head>
<body>
    <div id="keret">
        <h1>Regisztráció</h1>
        <form action="regisztracio.php" method="POST">
            Név: <input type=text" name="nev" required/> <br/><br/>
            E-mail: <input type=email" name="email" required /> <br/><br/>
            Telefonszám: <input type="text" name="tel" /> <br/><br/>
            Jelszó: <input type="password" name="jelszo" /> <br/><br/>
            Jelszó újra: <input type="password" name="jelszo2" /> <br/><br/>
            <input type="submit" value="Regisztráció" />
        </form>
    </div>    
    
    <?php
        if(isset($_GET['err']) && $_GET['err']=="jelszo") {
            echo "<h2>Nem egyezik a két jelszó!</h2>";
        }
        else if(isset($_GET['reg']) && $_GET['reg']=="ok") {
            echo "<h2>Sikeres reg.</h2>";
        }
        else if(isset($_GET['reg']) && $_GET['reg']=="err") {
            echo "<h2>Sikertelen reg egyébb hiba miatt.</h2>";
        }
    ?>
</body>
</html>
