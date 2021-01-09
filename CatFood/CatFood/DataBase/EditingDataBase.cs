using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatFood
{
    public class EditingDataBase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(ConnectSQLite.DatabasePath, ConnectSQLite.Flags);
        });
        public static SQLiteAsyncConnection Database => lazyInitializer.Value;

        static bool initialized = false;

        public EditingDataBase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ClockFood).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(ClockFood)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<ClockFood>> GetItemsAsync()
        {
            return Database.Table<ClockFood>().ToListAsync();
        }
        public Task<int> SaveItemAsync(ClockFood item)
        {
            return Database.InsertAsync(item);
        }
        public Task<int> DeleteItemAsync(ClockFood item)
        {
            return Database.DeleteAsync(item);
        }

    }
}
