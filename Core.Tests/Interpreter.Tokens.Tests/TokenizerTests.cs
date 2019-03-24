using Core.Interpreter;
using Core.Interpreter.Tokens;
using Core.Tests.Interpreter.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Core.Tests.Interpreter.Tokens.Tests
{
	[TestClass]
	public class TokenizerTests
	{
		[TestMethod]
		public void IdkSomeWeirdTest()
		{
			StringStreamReader reader = new StringStreamReader("testing 123 : , ( ) \"asd\"");
			TokenType[] expectedTypes = { TokenType.Name, TokenType.Number, TokenType.Colon,
					TokenType.Comma, TokenType.LeftParen, TokenType.RightParen, TokenType.String
			};

			PeekBuffer buffer = new PeekBuffer(reader);

			List<ITokenReader> tokenReaders = new List<ITokenReader>();
			tokenReaders.Add(new WhiteSpaceReader());
			tokenReaders.Add(new NameReader());
			tokenReaders.Add(new NumberReader());
			tokenReaders.Add(new StringTokenReader());
			tokenReaders.Add(new SingleSymbolReader(':', TokenType.Colon));
			tokenReaders.Add(new SingleSymbolReader(',', TokenType.Comma));
			tokenReaders.Add(new SingleSymbolReader('(', TokenType.LeftParen));
			tokenReaders.Add(new SingleSymbolReader(')', TokenType.RightParen));

			Tokenizer tokenizer = new Tokenizer(buffer, tokenReaders);
			for (int i = 0; i < expectedTypes.Length; i++)
			{
				Assert.AreEqual(expectedTypes[i].ToString(), tokenizer.GetToken().Type.ToString());
			}
			Assert.IsTrue(tokenizer.EndOfFile);
		}

		[TestMethod]
		public void ReaderCallSequential()
		{
			StringStreamReader reader = new StringStreamReader("abc");
			PeekBuffer buffer = new PeekBuffer(reader);

			List<ITokenReader> tokenReaders = new List<ITokenReader>();
			var testReader1 = new TestReader();
			var testReader2 = new TestReader();
			var testReader3 = new TestReader();
			tokenReaders.Add(testReader1);
			tokenReaders.Add(testReader2);
			tokenReaders.Add(testReader3);

			var tokenizer = new Tokenizer(buffer, tokenReaders);
			tokenizer.GetToken();

			Assert.IsTrue(testReader1.IsStartingCalled < testReader2.IsStartingCalled);
			Assert.IsTrue(testReader2.IsStartingCalled < testReader3.IsStartingCalled);
		}

		[TestMethod]
		public void ReaderCallIsStarting_True()
		{
			StringStreamReader reader = new StringStreamReader("abc");
			PeekBuffer buffer = new PeekBuffer(reader);

			List<ITokenReader> tokenReaders = new List<ITokenReader>();
			var testReader = new TestReader();
			testReader.IsStarting = true;
			tokenReaders.Add(testReader);
			var tokenizer = new Tokenizer(buffer, tokenReaders);
			tokenizer.GetToken();

			Assert.AreEqual(0, testReader.IsStartingCalled);
			Assert.AreEqual(1, testReader.CheckTokenCalled);
		}

		[TestMethod]
		public void ReaderCallIsStarting_False()
		{
			StringStreamReader reader = new StringStreamReader("abc");
			PeekBuffer buffer = new PeekBuffer(reader);

			List<ITokenReader> tokenReaders = new List<ITokenReader>();
			var testReader = new TestReader();
			testReader.IsStarting = false;
			tokenReaders.Add(testReader);

			var tokenizer = new Tokenizer(buffer, tokenReaders);
			tokenizer.GetToken();

			Assert.AreEqual(0, testReader.IsStartingCalled);
			Assert.AreEqual(-1, testReader.CheckTokenCalled);
		}

		[TestMethod]
		public void ReaderCallCheckToken_True()
		{
			StringStreamReader reader = new StringStreamReader("abc");
			PeekBuffer buffer = new PeekBuffer(reader);

			List<ITokenReader> tokenReaders = new List<ITokenReader>();
			var testReader = new TestReader();
			testReader.IsStarting = true;
			testReader.IsToken = true;
			tokenReaders.Add(testReader);

			var tokenizer = new Tokenizer(buffer, tokenReaders);
			tokenizer.GetToken();

			Assert.AreEqual(0, testReader.IsStartingCalled);
			Assert.AreEqual(1, testReader.CheckTokenCalled);
			Assert.AreEqual(2, testReader.ReadTokenCalled);
		}

		[TestMethod]
		public void ReaderCallCheckToken_False()
		{
			StringStreamReader reader = new StringStreamReader("abc");
			PeekBuffer buffer = new PeekBuffer(reader);

			List<ITokenReader> tokenReaders = new List<ITokenReader>();
            var testReader = new TestReader {IsStarting = true, IsToken = false};
            tokenReaders.Add(testReader);

			var tokenizer = new Tokenizer(buffer, tokenReaders);
			tokenizer.GetToken();

			Assert.AreEqual(0, testReader.IsStartingCalled);
			Assert.AreEqual(1, testReader.CheckTokenCalled);
			Assert.AreEqual(-1, testReader.ReadTokenCalled);
		}
	}

	public class TestReader : ITokenReader
	{
		public static int CallOrder = 0;
		public bool IsStarting { get; set; }
		public bool IsToken { get; set; }
		public int CharsToRead { get; set; }
		public bool ReturnTokenInfo { get; set; }
		public TokenType Type { get; set; }
		public object Data { get; set; } = null;

		public int IsStartingCalled { get; private set; } = -1;
		public int CheckTokenCalled { get; private set; } = -1;
		public int ReadTokenCalled { get; private set; } = -1;

		public TestReader(bool isStarting = false, bool isToken = false, 
			int charsToRead = 1, bool returnTokenInfo = true, 
			TokenType type = TokenType.Undefined, dynamic data = null)
		{
			IsStarting = isStarting;
			IsToken = isToken;
			CharsToRead = charsToRead;
			ReturnTokenInfo = returnTokenInfo;
			Type = type;
			Data = data;
			CallOrder = 0;
		}

		public bool IsStartingChar(char c)
		{
			IsStartingCalled = CallOrder++;
			return IsStarting;
		}

		public bool CheckToken(PeekBuffer buffer)
		{
			CheckTokenCalled = CallOrder++;
			return IsToken;
		}

		public TokenInfo ReadToken(PeekBuffer buffer)
		{
			ReadTokenCalled = CallOrder++;

			for (int i = 0; i < CharsToRead; i++)
			{
				buffer.Read();
			}

			if (ReturnTokenInfo)
			{
				if (Data != null)
				{
					return new TokenInfo(Type, Data);
				}
				return new TokenInfo(Type);
			}
			return null;
		}
	}
}
