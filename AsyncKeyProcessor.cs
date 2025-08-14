using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Szybkopis
{
    public class AsyncKeyProcessor
    {
        private readonly PolishCharMapper _mapper;
        private readonly SemaphoreSlim _semaphore;
        private volatile bool _processing = false;

        public AsyncKeyProcessor(PolishCharMapper mapper)
        {
            _mapper = mapper;
_semaphore = new SemaphoreSlim(1, 1);
        }

        public async Task<bool> ProcessKeyAsync(Keys key, bool isShift, bool isCtrl, bool isAlt)
        {
if (_processing) return false;

            await _semaphore.WaitAsync();
            try
            {
                _processing = true;
                
                var result = _mapper.ProcessKey(key, isShift, isCtrl, isAlt);
                if (result != null)
                {
_ = Task.Run(async () =>
                    {
                        try
                        {
                            await FastInputSender.SendTextAsync(result);
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Error sending input: {ex}");
                        }
                    });
                    
                    return true;
                }
                
return false;
            }
            finally
            {
                _processing = false;
                _semaphore.Release();
            }
        }
    }
}