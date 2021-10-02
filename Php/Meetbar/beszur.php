<?php
include_once './menu.php';
include_once './abkapcs.php';
?>
<form action="#" method="post">
    <label for="nev">Új termék neve:</label><br>
    <input type="text" name="nev" required /><br>
    <label for="ar">Új termék ára:</label><br>
    <input type="number" name="ar" required /><br>
    <label for="csomagolas">Csomagolás:</label><br>
    <input type="radio" name="csomagolas" value="1" checked="checked" />Van<br>
    <input type="radio" name="csomagolas" value="0" />Nincs<br>
    <label for="kep">Kép:</label><br>
    <input type="file" name="kep" value="" width="150" required/><br>
    <input type="submit" value="Szúrd be!" name="submit"/>
</form>
<?php
    if(isset($_POST['submit'])){
        $nev=$_POST['nev'];
        $ar=$_POST['ar'];
        $csomagolas=$_POST['csomagolas'];
        $kep=$_POST['kep'];
        $sql7="INSERT INTO kinalat(ar,csomagolas,nev,kep) VALUES(".$ar.",".$csomagolas.",'".$nev."','".$kep."')";
        $lekerdezes=$kapcsolat->query($sql7) or die('Hiba a beszúrásnál!');
        echo 'Sikeres feltöltés!';
    }
?>