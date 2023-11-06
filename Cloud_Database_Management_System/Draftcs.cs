//[HttpGet]
//[ProducesResponseType(StatusCodes.Status200OK)]
//public ActionResult<IEnumerable<DatabaseDTO>> GetDatabases()
//{
//    return Ok(DatabaseStore.DatabaseList);
//}

//[HttpGet("{Name_Identify}/{id:int}", Name = "GetDatabase")]
//[ProducesResponseType(StatusCodes.Status200OK)]
//[ProducesResponseType(StatusCodes.Status400BadRequest)]
//[ProducesResponseType(StatusCodes.Status404NotFound)]
//public ActionResult<DatabaseDTO> GetDatabases(string Name_Identify, [FromBody] object dataObject, int id)
//{
//    if (id == 0)
//    {
//        return BadRequest();
//    }

//    Dictionary<string, Type> dataClassMap = new Dictionary<string, Type>
//    {
//        { "Group1", typeof(Group1_Data_Model) },
//        { "Group2", typeof(Group2_Data_Model) },
//        { "Group3", typeof(Group3_Data_Model) },
//        { "Group4", typeof(Group4_Data_Model) },
//        { "Group5", typeof(Group5_Data_Model) },
//        { "Group6", typeof(Group6_Data_Model) }
//    };

//    if (dataClassMap.TryGetValue(Name_Identify, out Type dataClassType))
//    {
//        // Use System.Text.Json to deserialize the dataObject into the corresponding data class
//        try
//        {
//            var data = JsonSerializer.Deserialize(JsonSerializer.Serialize(dataObject), dataClassType);
//            return Ok(data);
//        }
//        catch (JsonException)
//        {
//            return BadRequest();
//        }
//    }
//    else
//    {
//        return NotFound();
//    }
//}

//[HttpPost("group1")]
//public IActionResult ProcessGroup1Data(int groupId, [FromBody] object data)
//{
//    if (!_groupService.TryProcessData(groupId, data, out var result))
//    {
//        return BadRequest("Invalid data for the specified group.");
//    }

//    return Ok(result);
//}

//[HttpPost("group2")]
//public IActionResult ProcessGroup2Data(int groupId, [FromBody] object data)
//{
//    if (!_groupService.TryProcessData(groupId, data, out var result))
//    {
//        return BadRequest("Invalid data for the specified group.");
//    }

//    return Ok(result);
//}

//[HttpPost("group3")]
//public IActionResult ProcessGroup3Data(int groupId, [FromBody] object data)
//{
//    if (!_groupService.TryProcessData(groupId, data, out var result))
//    {
//        return BadRequest("Invalid data for the specified group.");
//    }
//    // Use System.Text.Json to deserialize the dataObject into the corresponding data class
//    try
//    {
//        var data_ = JsonSerializer.Deserialize(JsonSerializer.Serialize(data), typeof(Group3_Data_Model));
//        return Ok(data_);
//    }
//    catch (JsonException)
//    {
//        return BadRequest();
//    }
//}

//[HttpPost("group4")]
//public IActionResult ProcessGroup4Data(int groupId, [FromBody] object data)
//{
//    if (!_groupService.TryProcessData(groupId, data, out var result))
//    {
//        return BadRequest("Invalid data for the specified group.");
//    }

//    return Ok(result);
//}

//[HttpPost("group5")]
//public IActionResult ProcessGroup5Data(int groupId, [FromBody] object data)
//{
//    if (!_groupService.TryProcessData(groupId, data, out var result))
//    {
//        return BadRequest("Invalid data for the specified group.");
//    }

//    return Ok(result);
//}

//[HttpPost("group6")]
//public IActionResult ProcessGroup6Data(int groupId, [FromBody] object data)
//{
//    if (!_groupService.TryProcessData(groupId, data, out var result))
//    {
//        return BadRequest("Invalid data for the specified group.");
//    }

//    return Ok(result);
//}