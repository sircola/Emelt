<!DOCTYPE html>
<?php
include_once 'kapcsolat.php';
?>
<html>
    <head>
        <meta charset="UTF-8" lang="hu-HU">
        <title>Határidőnapló</title>
    </head>
    <body>
        <p align="center"><h2>Határidő napló</h2></p>
    <table>
        <th>
            <td>Napszak</td>
            <td>Délelőtt</td>
            <td>Délután</td>
            <td>Este</td>
        </th>
        <tr>
            <td>8:00</td>
        </tr>
        <tr>
            <td>9:00</td>
        </tr>
        <tr>
            <td>10:00</td>
        </tr>
        <tr>
            <td>11:00</td>
        </tr>
        <tr>
            <td>13:00</td>
        </tr>
        <tr>
            <td>14:00</td>
        </tr>
        <tr>
            <td>15:00</td>
        </tr>
        <tr>
            <td>16:00</td>
        </tr>
        <tr>
            <td>17:00</td>
        </tr>
        <tr>
            <td>19:00</td>
        </tr>
    </table>
    <table>
    <th>
        <td>Bejegyzések</td>
        <td>Hétfő</td>
        <td>Kedd</td>
        <td>Szerda</td>
        <td>Csütörtök</td>
        <td>Péntek</td>
        <td>Szombat</td>
        <td>Vasárnap</td>
    </th>
    <tr>
        <td>Délelőtt</td>
    </tr>
    <tr>
        <td>Délelőtt</td>
    </tr>
    <tr>
        <td>Délelőtt</td>
    </tr>
    <tr>
        <td>Délelőtt</td>
    </tr>
    <tr>
        <td>Délután</td>
    </tr>
    <tr>
        <td>Délután</td>
    </tr>
    <tr>
        <td>Délután</td>
    </tr>
    <tr>
        <td>Délután</td>
    </tr>
    <tr>
        <td>Délután</td>
    </tr>
    <tr>
        <td>Este</td>
    </tr>
</table>
<div>
    <form action="#" method="post">
        <fieldset>
            <label for="délelőtt">Délelőtt</label>
            <input type="radio" name="8" id="8">
            <input type="radio" name="9" id="9">
            <input type="radio" name="10" id="10">
            <input type="radio" name="11" id="11">
            <br>
            <label for="délután">Délután</label>
            <input type="radio" name="13" id="13">
            <input type="radio" name="14" id="14">
            <input type="radio" name="15" id="15">
            <input type="radio" name="16" id="16">
            <input type="radio" name="17" id="17">
            <br>
            <label for="este">Este</label>
            <input type="radio" name="19" id="19">
            <br>
        </fieldset>
        <fieldset>
            <label for="hétfő">Hétfő</label>
            <br>
            <input type="radio" name="délelőtt" id="délelőtt">
            <input type="radio" name="délután" id="délután">
            <input type="radio" name="este" id="este">
            <br>
            <label for="kedd">Kedd</label>
            <br>
            <input type="radio" name="délelőtt" id="délelőtt">
            <input type="radio" name="délután" id="délután">
            <input type="radio" name="este" id="este">
            <br>
            <label for="szerda">Szerda</label>
            <br>
            <input type="radio" name="délelőtt" id="délelőtt">
            <input type="radio" name="délután" id="délután">
            <input type="radio" name="este" id="este">
            <br>
            <label for="csütörtök">Csütörtök</label>
            <br>
            <input type="radio" name="délelőtt" id="délelőtt">
            <input type="radio" name="délután" id="délután">
            <input type="radio" name="este" id="este">
            <br>
            <label for="péntek">Péntek</label>
            <br>
            <input type="radio" name="délelőtt" id="délelőtt">
            <input type="radio" name="délután" id="délután">
            <input type="radio" name="este" id="este">
            <br>
            <label for="szombat">Szombat</label>
            <br>
            <input type="radio" name="délelőtt" id="délelőtt">
            <input type="radio" name="délután" id="délután">
            <input type="radio" name="este" id="este">
            <br>
            <label for="vasárnap">Vasárnap</label>
            <br>
            <input type="radio" name="délelőtt" id="délelőtt">
            <input type="radio" name="délután" id="délután">
            <input type="radio" name="este" id="este">
            <br>
        </fieldset>
        <br>
        <input type="submit" value="Elküld">
    </form>
</div>
<?php
$sql = "SELECT * FROM bejegyzések";
$adatok = mysqli_query($kapcsolatok, $sql);
echo "<table>";
echo '<tr>';
echo '<td>$sor[délelőtt]</td>';
echo '<td>$sor[délután]</td>';
echo '<td>$sor[este]</td>';
echo '</tr>';
while ($sor = mysqli_fetch_array($adatok)) {
    echo '<tr>';
    echo '<td>$sor[délelőtt]</td>';
    echo '<td>$sor[délután]</td>';
    echo '<td>$sor[este]</td>';
    echo '</tr>';
}
echo '</table>';
echo '<table>';
echo '<tr>';
echo '<td>$sor[délelőtt]</td>';
echo '<td>$sor[délután]</td>';
echo '<td>$sor[este]</td>';
echo '</tr>';
while ($sor = mysqli_fetch_array($adatok)) {
    echo '<tr>';
    echo '<td>$sor[délelőtt]</td>';
    echo '<td>$sor[délután]</td>';
    echo '<td>$sor[este]</td>';
    echo '</tr>';
}
echo '</table>';
?>
</body>
</html>