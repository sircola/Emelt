<?php
include_once './menu.php'; 
include_once './abkapcs.php';
?>
<form action="#" method="post">
    <label for="torlendo">Termék:</label>
    <select name="torlendo">
        <?php
        $sql4 = "SELECT nev,ar,termekID FROM kinalat";
        $lekerdezes = $kapcsolat->query($sql4) or die('Hiba a 4.lekérdezésnél');
        while ($sor = $lekerdezes->fetch_assoc()) {
            echo '<option value='.$sor["termekID"].'>' .$sor['nev'].'  '.$sor['ar'].' Ft'. '</option>';
        }
        $lekerdezes->close();
        ?>
        <input type="submit" value="Töröld!" name="submit" />
    </select>
</form>
<?php
if(isset($_POST['submit'])){
    $id=$_POST['torlendo'];
    $sql6='SELECT * FROM kinalat WHERE termekID='.$id;
    $lekerdezes=$kapcsolat->query($sql6) or die('Hiba a 6.lekérdezésnél!');
    $sor=$lekerdezes->fetch_assoc();
    $sql5="DELETE FROM kinalat WHERE termekID=".$id;
    $lekerdezes=$kapcsolat->query($sql5) or die('Hiba a törlésnél!');
    echo $sor['termekID']," számú, ",$sor['nev']," nevű, ",$sor['ar'],' árú termék sikeresen törölve!';
}

?>