using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    public class MethodInterception : MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation)
        {
           
        }
        protected virtual void OnAfter(IInvocation invocation)
        {

        }
        protected virtual void OnException(IInvocation invocation,Exception e)
        {

        }
        protected virtual void OnSuccess(IInvocation invocation)
        {

        }

        public override void Intercept(IInvocation invocation)
        {
            var isSucccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
                
            }
            catch (Exception ex)
            {
                isSucccess = false;
                OnException(invocation,ex);
                throw;
            }
            finally
            {
                if (isSucccess)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);
        }
    }
}
