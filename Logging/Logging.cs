using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            PathfinderRunner pathfinderRunner = new PathfinderRunner(loggerFactory);
            await pathfinderRunner.RunAsync();
        }
    }

    class PathfinderRunner
    {
        private readonly LoggerFactory _loggerFactory;

        public PathfinderRunner(LoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task RunAsync()
        {
            List<Task> tasks = new List<Task>
            {
                new Pathfinder(_loggerFactory.CreateFileLogger()).FindAsync(),
                new Pathfinder(_loggerFactory.CreateConsoleLogger()).FindAsync(),
                new Pathfinder(_loggerFactory.CreateSecureLogger(_loggerFactory.CreateFileLogger())).FindAsync(),
                new Pathfinder(_loggerFactory.CreateSecureLogger(_loggerFactory.CreateConsoleLogger())).FindAsync(),
                new Pathfinder(_loggerFactory.CreateMultiLogger(
                    _loggerFactory.CreateConsoleLogger(),
                    _loggerFactory.CreateSecureLogger(_loggerFactory.CreateFileLogger())
                )).FindAsync()
            };

            await Task.WhenAll(tasks);
        }
    }

    class Pathfinder
    {
        private readonly ILogger _logger;
        private readonly string _log;

        public Pathfinder(ILogger logger, string log = "Log")
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _log = log;
        }

        public async Task FindAsync()
        {
            await _logger.LogErrorAsync(_log);
        }
    }

    class ConsoleLogger : ILogger
    {
        public Task LogErrorAsync(string message)
        {
            return Task.Run(() => Console.WriteLine(message));
        }
    }

    class FileLogger : ILogger, IDisposable
    {
        private readonly StreamWriter _writer;

        public FileLogger(string file = "log.txt")
        {
            _writer = new StreamWriter(file, true);
        }

        public async Task LogErrorAsync(string message)
        {
            await _writer.WriteLineAsync(message);
        }

        public void Dispose()
        {
            _writer.Dispose();
        }
    }

    class SecureLogger : ILogger
    {
        private readonly ILogger _logger;
        private readonly DayOfWeek _day;

        public SecureLogger(ILogger logger, DayOfWeek day = DayOfWeek.Friday)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _day = day;
        }

        public async Task LogErrorAsync(string message)
        {
            if (DateTime.Now.DayOfWeek == _day)
                await _logger.LogErrorAsync(message);
        }
    }

    class MultiLogger : ILogger
    {
        private readonly ILogger[] _loggers;

        public MultiLogger(params ILogger[] loggers)
        {
            _loggers = loggers ?? throw new ArgumentNullException(nameof(loggers));
        }

        public async Task LogErrorAsync(string message)
        {
            List<Task> tasks = new List<Task>();

            foreach (var logger in _loggers)
                tasks.Add(logger.LogErrorAsync(message));

            await Task.WhenAll(tasks);
        }
    }

    class LoggerFactory
    {
        public ILogger CreateFileLogger()
        {
            return new FileLogger();
        }

        public ILogger CreateConsoleLogger()
        {
            return new ConsoleLogger();
        }

        public ILogger CreateSecureLogger(ILogger logger)
        {
            return new SecureLogger(logger);
        }

        public ILogger CreateMultiLogger(params ILogger[] loggers)
        {
            return new MultiLogger(loggers);
        }
    }

    interface ILogger
    {
        Task LogErrorAsync(string message);
    }
}