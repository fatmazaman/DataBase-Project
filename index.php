<?php
// if(!function_exists('dbopen')){
//         include ('dbcon.php');
//     }
//     $db = dbopen();
//     $sql= "SELECT Name from Herb";
//     $result = $db->query($sql);
    // $rows = mysqli_fetch_array($result)
?>
<!DOCTYPE html>
<html lang="en">
    <head>
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <title>Herb Disease</title>
        <!-- CSS -->
        <!-- <link rel="stylesheet" href="http://fonts.googleapis.com/css?family=PT+Sans:400,700"> -->
        <link rel="stylesheet" href="assets/css/bootstrap.css">
        <link rel="stylesheet" href="assets/css/bootstrap.min.css">
        <link rel="stylesheet" type="text/css" href="assets/css/custom.css">
    </head>
    <body>
    
        <div class="container">
            <div class="row">

            <div class="col-md-2">
                <form method="post" action="./query.php">
                  <input type="text" name="Herb_Name">
                  <input type="submit" class="btn btn-lg">
                </form>
                </div>
        </div>
        </div>
        <!-- Javascript -->
        <script src="assets/js/jquery-1.10.2.min.js"></script>
        <!-- <script src="assets/js/jquery.backstretch.min.js"></script> -->
        <script src="assets/js/scripts.js"></script>
    </body>
</html>