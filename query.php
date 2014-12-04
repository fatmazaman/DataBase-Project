<?php
if(!function_exists('dbopen')){
        include ('dbcon.php');
    }
    $db = dbopen();
    $HerbName = $_POST['Herb_Name'];
    $query = "SELECT CID from Herb2Comp where HerbName='$HerbName'";
    $result = $db->query($query);
    $ComponentIDs = mysqli_fetch_array($result);

    //Array of CID, Get first CID
    $CID = explode(',', $ComponentIDs[0]);
    // echo $CID[0];

    $query2="SELECT * from Drug2Gene inner join Component2DrugID on Component2DrugID.CID='$CID[0]' && Drug2Gene.DID=Component2DrugID.DID";
    $result2 = $db->query($query2);
    $row = mysqli_fetch_array($result2);
    $GeneSymbol = $row['GSymbol'];
    // echo $GeneSymbol;

    $query3 = "SELECT * from MESH2Disease inner join Gene2MeSH on Gene2MeSH.GSymbol='$GeneSymbol' && MESH2Disease.MESHID=Gene2MeSH.MESHID";
    $result3 = $db->query($query3);
    $row2 = mysqli_fetch_array($result3);
    $DiseaseName = $row2['DiseaseName'];
    echo $DiseaseName;


?>