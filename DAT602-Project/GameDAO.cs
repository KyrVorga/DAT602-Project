using Battlespire;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlespire
{
    internal class GameDAO : DatabaseAccessObject
    {

        public List<string> GetChat()
        {
            try
            {
                DataSet chat_logs = MySqlHelper.ExecuteDataset(DatabaseAccessObject.MySqlConnection, "call GetChatHistory();");

                var chat_messages = new List<string>();
                foreach (DataRow row in chat_logs.Tables[0].Rows)
                {
                    chat_messages.Add(row[0].ToString());
                }

                return chat_messages;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetLeaderboard()
        {
            try
            {
                DataSet leaderboard_raw = MySqlHelper.ExecuteDataset(DatabaseAccessObject.MySqlConnection, "call GetLeaderboard();");

                var leaderboard_list = new List<string>();
                foreach (DataRow row in leaderboard_raw.Tables[0].Rows)
                {
                    leaderboard_list.Add(row[0].ToString());
                }

                return leaderboard_list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Boolean CheckIsAdmin(string username)
        {
            try
            {
                List<MySqlParameter> procedure_params = new()
                {
                    new()
                    {
                        ParameterName = "@username",
                        MySqlDbType = MySqlDbType.VarChar,
                        Size = 50,
                        Value = username
                    }

                };

                DataSet query_result = MySqlHelper.ExecuteDataset(MySqlConnection, "call IsAdminAccount(@username)", procedure_params.ToArray());

                DataRow row = query_result.Tables[0].Rows[0];
                
                if (row != null)
                {
                    if ((bool)row[0] == true)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<Tile> GetTilesByPlayer(int player_id)
        {
            List<Tile> tile_list = new();
            try
            {
                List<MySqlParameter> procedure_params = new()
                {
                    new()
                    {
                        ParameterName = "@player_id",
                        MySqlDbType = MySqlDbType.Int32,
                        Value = player_id
                    },
                    new()
                    {
                        ParameterName = "@viewport_width",
                        MySqlDbType = MySqlDbType.Int32,
                        Value = 10
                    },
                    new()
                    {
                        ParameterName = "@viewport_height",
                        MySqlDbType = MySqlDbType.Int32,
                        Value = 10
                    }

                };

                DataSet query_result = MySqlHelper.ExecuteDataset(MySqlConnection, "call GetTilesByPlayer(@player_id, @viewport_width, @viewport_height)", procedure_params.ToArray());
            
                foreach (DataRow row in query_result.Tables[0].Rows)
                {
                
                    var newTile = new BoardTile((int)row[0], (int)row[1], (int)row[2], (string)row[3]);
                    tile_list.Add(newTile);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
              
            return tile_list;
        }

        public void PlayerExit(int player_id)
        {
            try
            {
                List<MySqlParameter> procedure_params = new()
                {
                    new()
                    {
                        ParameterName = "@player_id",
                        MySqlDbType = MySqlDbType.Int32,
                        Value = player_id
                    }
                };

                MySqlHelper.ExecuteDataset(MySqlConnection, "call PlayerWin(@player_id)", procedure_params.ToArray());

                Game.Mainform.ReloadGame();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Entity> LoadEntities(int playerId)
        {
            try
            {
                List<MySqlParameter> procedure_params = new()
                {
                    new()
                    {
                        ParameterName = "@player_id",
                        MySqlDbType = MySqlDbType.Int32,
                        Value = playerId
                    }
                };

                DataSet query_result = MySqlHelper.ExecuteDataset(DatabaseAccessObject.MySqlConnection, "call GetAllEntities(@player_id)", procedure_params.ToArray());

                var entity_list = new List<Entity>();
                foreach (DataRow row in query_result.Tables[0].Rows)
                {
                    if ((string)row["entity_type"] == "player")
                    {
                        Entity newEntity = new Player((int)row["entity_id"], (int)row["health"], (int)row["damage_taken"], (int)row["attack"], (int)row["defense"], (int)row["healing"], (int)row["account_id"], (string)row["entity_type"], (int)row["tile_id"], (int)row["killscore"]);
                        entity_list.Add(newEntity);
                    }
                    else if ((string)row["entity_type"] == "monster")
                    {
                        Entity newEntity = new Monster((int)row["entity_id"], (string)row["name"], (int)row["health"], (int)row["damage_taken"], (int)row["attack"], (int)row["defense"], (int)row["healing"], (string)row["entity_type"], (int)row["tile_id"]);
                        entity_list.Add(newEntity);
                    }
                    else if ((string)row["entity_type"] == "chest")
                    {
                        Entity newEntity = new Chest((int)row["entity_id"], (string)row["entity_type"], (int)row["tile_id"]);
                        entity_list.Add(newEntity);
                    }
                }
                return entity_list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Player LoadPlayer(string username)
        {
            try
            {
                List<MySqlParameter> procedure_params = new()
                {
                    new()
                    {
                        ParameterName = "@username",
                        MySqlDbType = MySqlDbType.VarChar,
                        Size = 50,
                        Value = username
                    }
                };

                DataSet query_result = MySqlHelper.ExecuteDataset(DatabaseAccessObject.MySqlConnection, "call GetPlayerByAccUsername(@username)", procedure_params.ToArray());

                DataRow row = query_result.Tables[0].Rows[0];

                Player newEntity = new Player((int)row["entity_id"], (int)row["health"], (int)row["damage_taken"], (int)row["attack"], (int)row["defense"], (int)row["healing"], (int)row["account_id"], (string)row["entity_type"], (int)row["tile_id"], (int)row["killscore"]);

                return newEntity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MovePlayer(int target_tile, int entity_id)
        {
            try
            {
                List<MySqlParameter> procedure_params = new()
                {
                    new()
                    {
                        ParameterName = "@target_tile",
                        MySqlDbType = MySqlDbType.Int32,
                        Value = target_tile
                    },
                    new()
                    {
                        ParameterName = "@player_id",
                        MySqlDbType = MySqlDbType.Int32,
                        Value = entity_id
                    }
                };

                MySqlHelper.ExecuteDataset(DatabaseAccessObject.MySqlConnection, "call MovePlayer(@target_tile, @player_id)", procedure_params.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Item> GetEntityInventory(int entity_id)
        {
            try
            {
                List<MySqlParameter> procedure_params = new()
                {
                    new()
                    {
                        ParameterName = "@entity_id",
                        MySqlDbType = MySqlDbType.Int32,
                        Value = entity_id
                    }
                };

                DataSet query_result = MySqlHelper.ExecuteDataset(DatabaseAccessObject.MySqlConnection, "call GetEntityInventory(@entity_id)", procedure_params.ToArray());

                var item_list = new List<Item>();
                foreach (DataRow row in query_result.Tables[0].Rows)
                {
                    Item newEntity = new Item((int)row["entity_id"], (string)row["name"], (string)row["entity_type"], (int)row["tile_id"], (int)row["owner_id"], (int)row["health"], (int)row["attack"], (int)row["defense"], (int)row["healing"], (bool)row["is_equipped"]);
                    item_list.Add(newEntity);
                }

                return item_list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Tile> GetPlayerInventoryTiles(int playerId)
        {
            try
            {
                List<MySqlParameter> procedure_params = new()
                {
                    new()
                    {
                        ParameterName = "@entity_id",
                        MySqlDbType = MySqlDbType.Int32,
                        Value = playerId
                    }
                };

                DataSet query_result = MySqlHelper.ExecuteDataset(DatabaseAccessObject.MySqlConnection, "call GetEntityInventoryTiles(@entity_id)", procedure_params.ToArray());

                var tile_list = new List<Tile>();
                foreach (DataRow row in query_result.Tables[0].Rows)
                {
                    if (row != null)
                    {
                        var newTile = new PlayerInventoryTile((int)row[0], (int)row[1], (int)row[2], (string)row[3], (int)row[4]);
                        tile_list.Add(newTile);
                    }
                }

                return tile_list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Tile> GetChestInventoryTiles(int chestId)
        {
            try
            {
                List<MySqlParameter> procedure_params = new()
                {
                    new()
                    {
                        ParameterName = "@entity_id",
                        MySqlDbType = MySqlDbType.Int32,
                        Value = chestId
                    }
                };

                DataSet query_result = MySqlHelper.ExecuteDataset(DatabaseAccessObject.MySqlConnection, "call GetEntityInventoryTiles(@entity_id)", procedure_params.ToArray());

                var tile_list = new List<Tile>();
                foreach (DataRow row in query_result.Tables[0].Rows)
                {
                    if (row != null)
                    {
                        var newTile = new ChestInventoryTile((int)row[0], (int)row[1], (int)row[2], (string)row[3], (int)row[4]);
                        tile_list.Add(newTile);
                    }
                }

                return tile_list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MoveInventoryItem(int _item_id, int _origin_tile_id, int _target_tile_id)
        {
            try
            {
                List<MySqlParameter> procedure_params = new()
                {
                    new()
                    {
                        ParameterName = "@item_id",
                        MySqlDbType = MySqlDbType.Int32,
                        Value = _item_id
                    },
                    new()
                    {
                        ParameterName = "@origin_tile_id",
                        MySqlDbType = MySqlDbType.Int32,
                        Value = _origin_tile_id
                    },
                    new()
                    {
                        ParameterName = "@target_tile_id",
                        MySqlDbType = MySqlDbType.Int32,
                        Value = _target_tile_id
                    }
                };

                MySqlHelper.ExecuteDataset(DatabaseAccessObject.MySqlConnection, "call MoveInventoryItem(@item_id, @origin_tile_id, @target_tile_id)", procedure_params.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EquipItem(int _player_id, int _item_id)
        {
            try
            {
                List<MySqlParameter> procedure_params = new()
                {
                    new()
                    {
                        ParameterName = "@item_id",
                        MySqlDbType = MySqlDbType.Int32,
                        Value = _item_id
                    },
                    new()
                    {
                        ParameterName = "@player_id",
                        MySqlDbType = MySqlDbType.Int32,
                        Value = _player_id
                    }
                };

                MySqlHelper.ExecuteDataset(DatabaseAccessObject.MySqlConnection, "call EquipItem(@player_id, @item_id)", procedure_params.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MoveMonster(int monster_id)
        {
            try
            {
                List<MySqlParameter> procedure_params = new()
                {
                    new()
                    {
                        ParameterName = "@monster_id",
                        MySqlDbType = MySqlDbType.Int32,
                        Value = monster_id
                    }
                };

                MySqlHelper.ExecuteDataset(DatabaseAccessObject.MySqlConnection, "call MoveMonsterNPC(@monster_id)", procedure_params.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void TransferItem(int itemId, int playerId)
        {
            try
            {
                List<MySqlParameter> procedure_params = new()
                {
                    new()
                    {
                        ParameterName = "@item_id",
                        MySqlDbType = MySqlDbType.Int32,
                        Value = itemId
                    },
                    new()
                    {
                        ParameterName = "@player_id",
                        MySqlDbType = MySqlDbType.Int32,
                        Value = playerId
                    }
                };

                MySqlHelper.ExecuteDataset(DatabaseAccessObject.MySqlConnection, "call TransferItem(@item_id, @player_id)", procedure_params.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DamageEntity(int attackerId, int defenderId)
        {
            try
            {
                List<MySqlParameter> procedure_params = new()
                {
                    new()
                    {
                        ParameterName = "@attacker_id",
                        MySqlDbType = MySqlDbType.Int32,
                        Value = attackerId
                    },
                    new()
                    {
                        ParameterName = "@defender_id",
                        MySqlDbType = MySqlDbType.Int32,
                        Value = defenderId
                    }
                };

                MySqlHelper.ExecuteDataset(DatabaseAccessObject.MySqlConnection, "call DamageEntity(@attacker_id, @defender_id)", procedure_params.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal Dictionary<string, decimal> GetPlayerStats(int entityId)
        {
            Dictionary<string, decimal> newStats = new();
            try
            {
                List<MySqlParameter> procedure_params = new()
                {
                    new()
                    {
                        ParameterName = "@entity_id",
                        MySqlDbType = MySqlDbType.Int32,
                        Value = entityId
                    }
                };

                DataSet query_result = MySqlHelper.ExecuteDataset(DatabaseAccessObject.MySqlConnection, "call GetPlayerStats(@entity_id)", procedure_params.ToArray());

                foreach (DataRow row in query_result.Tables[0].Rows)
                {
                    if (row != null)
                    {
                        newStats.Add("Health", (decimal)row[0]);
                        newStats.Add("Attack", (decimal)row[1]);
                        newStats.Add("Defense", (decimal)row[2]);
                        newStats.Add("Healing", (decimal)row[3]);
                        newStats.Add("DamageTaken", (decimal)row[4]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return newStats;
        }
    }
}
