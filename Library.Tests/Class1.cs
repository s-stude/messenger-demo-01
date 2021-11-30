using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using Library.Providers;
using Library.Providers.Impl;
using Xunit;

namespace Library.Tests
{
    // + библиотека для отправки сообщений
    // + отправляю в один провайдер
    // + отправляю в разные провайдеры
    // + сообщения всегда пишут дату кроме message
    // + сообщения имеют формат
    // + -- юзер может менять формат
    // + сообения имеют приоритет
    // + сообщения отправляются в разные хранилища по приоритету
    // + -- юзер может настраивать это правило

    public class Class1
    {
        [Fact]
        public void Messenger__Send__ShouldAbleGetMessageText()
        {
            var provider = A.Fake<DefaultProvider>();
            var messenger = new Messenger(provider);

            messenger.Send("hello world");
        }

        [Fact]
        public void Messenger__Send__ShouldUseDefaultProvider()
        {
            var provider = A.Fake<DefaultProvider>();
            var messenger = new Messenger(provider);

            messenger.Send("hello world");

            A.CallTo(() => provider.Write(A<string>.Ignored)).MustHaveHappened();
        }

        [Fact]
        public void Messenger__Send__ShouldUseManyProviders()
        {
            var provider1 = A.Fake<IProvider>();
            var provider2 = A.Fake<IProvider>();
            var provider3 = A.Fake<IProvider>();

            var messenger = new Messenger(new List<IProvider>()
            {
                provider1,
                provider2,
                provider3
            });

            messenger.Send("hello world");

            A.CallTo(() => provider1.Write(A<string>.Ignored)).MustHaveHappened();
            A.CallTo(() => provider2.Write(A<string>.Ignored)).MustHaveHappened();
            A.CallTo(() => provider3.Write(A<string>.Ignored)).MustHaveHappened();
        }

        [Fact]
        public void Messenger__Send__ShouldWriteDateAndMessage()
        {
            var provider1 = A.Fake<IProvider>();

            var messenger = new Messenger(provider1, "{date} {message}");

            messenger.Send("hello world");

            A.CallTo(() => provider1.Write("2021-11-30 hello world")).MustHaveHappened();
        }


        [Fact]
        public void Messenger__Send__ShouldWriteDateAndMessageInAFormat()
        {
            var provider1 = A.Fake<IProvider>();

            string format = "{message} ||| {date}";

            var messenger = new Messenger(provider1, format);

            messenger.Send("hello world");

            A.CallTo(() => provider1.Write("hello world ||| 2021-11-30")).MustHaveHappened();
        }

        [Fact]
        public void Messenger__Send__CanPassPriority()
        {
            var provider1 = A.Fake<IProvider>();

            string format = "{priority} | {message} | {date}";

            var messenger = new Messenger(provider1, format);

            messenger.Send("hello world");

            A.CallTo(() => provider1.Write("LOW | hello world | 2021-11-30")).MustHaveHappened();
        }

        [Fact]
        public void Messenger__Send__UseOnlyOneProviderMatchingPriority()
        {
            var provider1 = A.Fake<IProvider>();// low
            A.CallTo(() => provider1.Settings).Returns(new ProviderSettings(MessagePriority.Low));

            var provider2 = A.Fake<IProvider>(); // med
            A.CallTo(() => provider2.Settings).Returns(new ProviderSettings(MessagePriority.Medium));

            var messenger = new Messenger(new List<IProvider> { provider1, provider2 });

            messenger.Send("hello world");

            A.CallTo(() => provider1.Write(A<string>.Ignored)).MustHaveHappened();
            A.CallTo(() => provider2.Write(A<string>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void Messenger__Send__UseAllProvidersMatchingPriority()
        {
            var dbProvider = A.Fake<IProvider>();// low
            A.CallTo(() => dbProvider.Settings).Returns(new ProviderSettings(MessagePriority.Low));

            var fileProvider = A.Fake<IProvider>(); // low
            A.CallTo(() => fileProvider.Settings).Returns(new ProviderSettings(MessagePriority.High));

            var messenger = new Messenger(new List<IProvider>
            {
                dbProvider, fileProvider,
                new MyClass(new ProviderSettings(MessagePriority.Low))
            });

            messenger.Send(MessagePriority.Medium, "hello world");

            A.CallTo(() => dbProvider.Write(A<string>.Ignored)).MustHaveHappened();
            A.CallTo(() => fileProvider.Write(A<string>.Ignored)).MustHaveHappened();
        }
    }

    class MyClass : IProvider
    {
        public MyClass(IProviderSettings settings)
        {
            
        }
        public IProviderSettings Settings { get; }
        public void Write(string message)
        {
            throw new NotImplementedException();
        }
    }
}
