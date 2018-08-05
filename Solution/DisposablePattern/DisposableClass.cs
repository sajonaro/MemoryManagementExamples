// <copyright file="DisposableClass.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the DisposableClass type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DisposablePattern
{
    using System;
    using System.IO;

    class DisposableClass : IDisposable
    {
        /// <summary>
        /// The managed resource 
        /// </summary>
        TextReader tr = null;

        #region implementaion of IDisposable

        public DisposableClass(string path)
        {
            //Lets emulate the managed resource aquisition
            Console.WriteLine("Aquiring Managed Resources");
            this.tr = new StreamReader(path);

            //Lets emulate the unmabaged resource aquisition
            Console.WriteLine("Aquiring Unmanaged Resources");
        }

        void ReleaseManagedResources()
        {
            Console.WriteLine("Releasing Managed Resources");
            this.tr?.Dispose();
        }

        void ReleaseUnmangedResources()
        {
            Console.WriteLine("Releasing Unmanaged Resources");

            //e.g. pointer to OS handle
        }

        public void Dispose()
        {
            // If this function is being called it means - user wants to release the
            // resources. lets call the Dispose which will do this for us.
            Dispose(true);

            // Now since we have done the cleanup already there is nothing left
            // for the Finalizer to do. So lets tell the GC not to call it later.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// it is made virtual because DisposableClass is not marked as 'sealed'! 
        /// </summary>
        /// <param name="disposing">true - id called by user</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                //someone wants the deterministic release of all resources
                //Let us release all the managed resources
                ReleaseManagedResources();
            }
            else
            {
                // Do nothing, no one asked a dispose, the object went out of
                // scope and finalizer is called so let the next round of GC 
                // release these (managed) resources
            }

            // Release the unmanaged resource in any case as they will not be 
            // released by GC ( because GC has no knowledge about how exactly to
            // release the unmanaged resource!)
            ReleaseUnmangedResources();
        }

        /// <summary>
        /// Finalizer
        /// </summary>
        ~DisposableClass()
        {
            // The object went out of scope and finalized is called
            // 1)Calling dispose to release unmanaged resources 
            // 2)The managed resources anyways will get released when GC 
            // runs next time.
            Dispose(false);
        }


        #endregion

    }
}
