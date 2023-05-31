using System;
using System.Collections.Generic;
using System.Text;

namespace InitialTestApp
{
    class User
    {
        private bool isManager = false;

        public User(bool isManager)
        {
            this.isManager = isManager;
        }

        // Getter
        public bool IsManager()
        {
            return isManager;
        }

        // Setter
        // TODO: RENAME this function
        public void SetIsManager(bool isManager)
        {
            this.isManager = isManager;
        }
    }
}
