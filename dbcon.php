<?php
function dbopen(){
  $host="localhost"; // Host name 
  $username="root"; // Mysql username 
  $password="root"; // Mysql password 
  $db_name="Project4"; // Database name 

  //$tbl_name="donor"; // Table name 

  // Connect to server and select databse.
  $db= new mysqli($host, $username, $password, $db_name);
  if (!$db)
    {
    die('Could not connect: ' . mysql_error());
    }
  
  return($db);
}


function close(){
  mysqli_close();
  }

?>
