<?php
$host = "localhost";
$username = "root";
$password = "123";
$db_name = "Herb_Disease";

$db = new mysqli($host, $username, $password, $db_name);
$sql = "Select * from Component_Source";
$result = $db->query($sql);
//$rows = mysqli_fetch_array($result);
//fech_assoc -> associative array!(call tha data by attributes name)

foreach ($result as $row) 
	{?>

	<p> <?php echo $row['Name']; ?> &nbsp &nbsp <?php echo $row['Component']; ?> &nbsp&nbsp <?php echo $row['Source']; ?></p><br>

<?php
}
?> 
