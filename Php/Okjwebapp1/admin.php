<?php
    session_start();
?>
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <link href="formatumok.css" rel="stylesheet" />
    <title>Admin oldal</title>
</head>
<body>
    <?php
        if(isset($_SESSION['user'])) {
        
            $ab=new mysqli("localhost","phpmyadmin","bernie","webapp1");
            $ab->set_charset("utf8");
        
            if($ab->connect_error)
                die("Hiba a csatlakozáskor.");
            
            $sql = "SELECT * FROM felhasznalok";
            $er = $ab->query($sql);
            
            $ki = '';
            while($sor = $er->fetch_assoc()) {
                $nev = $sor['nev'];
                $ki .= "<form action='mentes.php' method='POST'>";
                $ki .=  $nev . ": ";
                $ki .= "<select name='statusz'>";
                $ki .= "<option value='0'>0</option>";
                $ki .= "<option value='1'>1</option>";
                $ki .= "<option value='2'>2</option>";
                $ki .= "</select>";
                $ki .= "<input type='hidden' name='nev' value='$nev' />";
                $ki .= "<input type='submit' value='Mentés' />";
                $ki .= "</form>";
            }
            echo $ki;
        }
        else {
    ?>
    
    <div id="keret">
        <h1>Belépés</h1>
        <form action="login.php" method="POST">
            Név: <input type=text" name="nev" required/> <br/><br/>
            Jelszó: <input type="password" name="jelszo" /> <br/><br/>
            <input type="submit" value="Belépés" />
        </form>
        
        <?php
            if(isset($_GET['err']) && $_GET['err']=="login") {
                echo "<h2>Rossz jelszó, név vagy jogosultság.</h2>";
            }
        ?>
    </div>   
    
    <?php } ?>
    
</body>
</html> 