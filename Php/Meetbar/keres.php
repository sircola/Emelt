<?php
include_once './menu.php';
include_once './abkapcs.php';
;?>
<form action="#" method="post">
    <label for="keresendo">Termék ID:</label>
    <select name="keresendo">
        <?php
        $sql2 = "SELECT termekID FROM kinalat";
        $lekerdezes = $kapcsolat->query($sql2) or die('Hiba a 2.lekérdezésnél');
        while ($sor = $lekerdezes->fetch_assoc()) {
            echo '<option>' . $sor['termekID'] . '</option>';
        }
        $lekerdezes->close();
        ?>
        <input type="submit" value="Mutasd!" name="submit" />
    </select>
</form>
<?php
if (isset($_POST['submit'])) {
    $id = $_POST['keresendo'];
    $sql3 = "SELECT * FROM kinalat WHERE termekID=" . $id;
    $lekerdezes = $kapcsolat->query($sql3) or die('Hiba a 3.lekérdezésnél');
    echo "<table>";
    echo "<th>Név</th><th>Ár</th><th>Csomagolás</th><th>Kép</th>";
    if ($lekerdezes->num_rows > 0) {
        echo "<tr>";
        $sor = $lekerdezes->fetch_assoc();
        echo "<td>" . $sor['nev'] . "</td><td>" . $sor['ar'] . "</td><td>" . $sor['csomagolas'] . "</td>";
        echo "<td><img src='" . $sor['kep'] . "' /></td>";
        echo "</tr>";
    } else {
        echo "Nincs találat!";
    }
    $lekerdezes->close();
}
echo "</table>";
?>