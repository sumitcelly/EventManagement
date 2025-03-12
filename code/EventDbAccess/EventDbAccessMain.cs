using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace EventDbAccess
{
  public class EventContext
  {
    public string ConnectionString { get; set; }

     
    public EventContext(string connectionString)
    {
      this.ConnectionString = connectionString;
    }

    private MySqlConnection GetConnection()
    {
      return new MySqlConnection(ConnectionString);
    }

    public List<Event> GetAllEvents()
    {
      List<Event> list = new List<Event>();

      using (MySqlConnection conn = GetConnection())
      {
        conn.Open();
        
        MySqlCommand cmd = new MySqlCommand("SELECT * FROM Events", conn);
        using (MySqlDataReader reader = cmd.ExecuteReader())
        {
          while (reader.Read())
          {
            list.Add(new Event()
            {
              EventId = reader.GetInt32("EventId"),
              EventName = reader.GetString("EventName"),
              EventDescription = reader.GetString("EventDescription"),
              EventDate = reader.GetDateTime("EventDate"),
              EventOrganizer = reader.GetString("EventOrganizer")
            });
          }
        }
      }

      return list;
    }
  
  }
}
