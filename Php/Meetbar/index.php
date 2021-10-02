<!DOCTYPE html>
<html>
    <head>
        <meta charset="UTF-8">
        <title>Meet Bar</title>
    </head>
    <body>
        <?php
        include_once './menu.php';
        include_once './abkapcs.php';
        $sql1 = "SELECT * FROM kinalat";
        $lekerdezes = $kapcsolat->query($sql1) or die("Hiba az 1.lekérdezésben");
        echo "<table>";
        echo "<th>Név</th><th>Ár</th><th>Csomagolás</th><th>Kép</th>";
        if($lekerdezes->num_rows>0){
            while($sor=$lekerdezes->fetch_assoc()){
                echo "<tr>";
                echo "<td>".$sor['nev']."</td><td>".$sor['ar']."</td><td>".$sor['csomagolas']."</td>";
                echo "<td><img src='".$sor['kep']."' /></td>";
                echo "</tr>";
            }
        }
        echo "</table>";
        $lekerdezes->close();
        ?>

    </body>
</html>
