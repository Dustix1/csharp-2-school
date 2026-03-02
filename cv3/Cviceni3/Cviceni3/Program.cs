namespace Cviceni3
{

    class SimpleStack<T>
    {

        private List<T> data = new List<T>();

        private object lockObject = new object();

        public T Top
        {
            get
            {
                lock (lockObject)
                {
                    int idx = this.data.Count - 1;
                    if (idx == -1)
                    {
                        throw new StackEmptyException();
                    }
                    return data[idx];
                }
            }
        }


        public bool IsEmpty
        {
            get
            {
                return this.data.Count == 0;
            }
        }


        public void Push(T val)
        {
            lock (lockObject)
            {
                this.data.Add(val);
            }
        }


        public bool TryPop(out T result)
        {
            T val;
            lock (lockObject) 
            {
                if (this.IsEmpty)
                {
                    result = default;
                    return false;
                }

                int idx = this.data.Count - 1;
                if (idx == -1)
                {
                    throw new StackEmptyException();
                }
                val = this.data[idx];
                this.data.RemoveAt(idx);
            }
            result = val;
            return true;
        }


        public class StackEmptyException : Exception
        {

        }
    }

    internal class Program
    {

        private static async Task Write()
        {
            /*Task t = File.WriteAllTextAsync("test.txt", "Nějaká data...");
            Console.WriteLine("zapsáno (není pravda)");
            return t;*/

            await File.WriteAllTextAsync("test.txt", "Nějaká data...");
        }


        static async Task Main(string[] args)
        {
            await Write();
            Console.WriteLine("Dokončeno..");

            /*
            task.ContinueWith(t =>
            {
                Console.WriteLine("Dokončeno..");
            });
            */


            /*object lockObject = new object();

            SimpleStack<int> stack = new SimpleStack<int>();
            Random rand = new Random();

            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    stack.Push(rand.Next());

                    lock (lockObject)
                    {
                        Monitor.Pulse(lockObject);
                    }

                    Thread.Sleep(100);
                }
            });
            thread.Start();

            for (int i = 0; i < 5; i++)
            {

                Thread t = new Thread(() =>
                {
                    while (true)
                    {
                        if (stack.TryPop(out var val))
                        {
                            Console.WriteLine($"{val} | TID: {Thread.CurrentThread.ManagedThreadId}");
                        } else
                        {
                            Console.WriteLine("Zásobník je prázdný");
                            lock (lockObject)
                            {
                                Monitor.Wait(lockObject);
                            }
                        }

                            Thread.Sleep(rand.Next(40, 1000));
                    }
                });

                t.Start();
            }*/




            /*object lockObject = new object();

             lock (lockObject)                // vysvětlení se mi nechce psát :3
             {

             }
            
             Monitor.Enter(lockObject);
             try
             {

             }
             finally
             {
                Monitor.Exit(lockObject);
             }*/



            /*SimpleStack<int> stack = new SimpleStack<int>();
            Random rand = new Random();

            for (int i = 0; i < 5; i++)
            {
                Thread t = new Thread(() =>
                {
                    while (true)
                    {
                        if (rand.NextDouble() < 0.7)
                        {
                            if (stack.TryPop(out int result))
                            {
                                Console.WriteLine(result);
                            } else
                            {
                                stack.Push(rand.Next());
                            }
                        }
                    }
                });

                t.Start();
            }*/



            /*int result = 0;

            Thread myThread = new Thread(() =>
            {
                int x = 0;
                int y = 10;

                result = x + y;
                while (true) { }
            });

            myThread.Start();
            //myThread.IsBackground = true;
            //myThread.Join();  // počká až vlákno dojede do konce

            Console.WriteLine(result);*/
        }
    }
}
