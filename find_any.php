<?php
if(isset($_POST['submit']))
{
	$input = $_POST['drug_bank'];
	$$host = "localhost";
    $username = "root";
    $password = "123";
    $db_name = "Herb_Disease";

    $sql = "Select * from DrugBank_GeneName"
}