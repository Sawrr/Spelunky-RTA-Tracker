using System;
using System.Runtime.InteropServices;

namespace AchievementsTracker
{
    class MemoryReader
    {
        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        private int[] SCREEN_STATE = { 0x1384B4, 0x58 };
        private int[] PLAYER_ONE_HEALTH = { 0x138558, 0x30, 0x280, 0 };
        private int[] PLAYER_TWO_HEALTH = { 0x138558, 0x30, 0x280, 0x14A4 };
        private int[] CHAR_SELECT = { 0x1384B4, 0x4C, 0x122BEC };
        private int[] DAMSEL_COUNT = { 0x138558, 0x30, 0x280, -0x70 };
        private int[] SHOPPIE_COUNT = { 0x138558, 0x30, 0x280, 0x6CB0 };
        private int[] CHARACTERS = { 0x1384B4, 0x4463EC };
        private int[] JOURNAL_PLACES = { 0x1384B4, 0x445DEC };
        private int[] JOURNAL_MONSTERS = { 0x1384B4, 0x445EEC };
        private int[] JOURNAL_ITEMS = { 0x1384B4, 0x445FEC };
        private int[] JOURNAL_TRAPS = { 0x1384B4, 0x4460EC };
        private int[] RUN_TIME = { 0x138558, 0x30, 0x280, 0x52AC };
        private int[] STAGE_TIME = { 0x138558, 0x30, 0x280, 0x52BC };
        private int[] LEVEL_IDX = { 0x138558, 0x30, 0x280, -0xC0 };
        private int[] SCORE = { 0x138558, 0x30, 0x280, 0x5298 };
        private int[] BOMBS = { 0x138558, 0x30, 0x280, 0x10 };
        private int[] PLAYS = { 0x1384B4, 0x4459C8 };
        private int[] TUNNEL_CHAPTER = { 0x1384B4, 0x445BE4 };
        private int[] TUNNEL_REMAINING = { 0x1384B4, 0x445BE8 };
        private int[] TUTORIAL_STATUS = { 0x1384B4, 0x445BE0 };

        private int processHandle;
        private int baseAddress;

        public MemoryReader(int processHandle, int baseAddress)
        {
            this.processHandle = processHandle;
            this.baseAddress = baseAddress;
        }

        public int ReadTutorialStatus()
        {
            byte[] buffer = new byte[1];
            ReadMemory(buffer, baseAddress, TUTORIAL_STATUS);
            return buffer[0];
        }

        public int ReadTunnelChapter()
        {
            byte[] buffer = new byte[1];
            ReadMemory(buffer, baseAddress, TUNNEL_CHAPTER);
            return buffer[0];
        }

        public int ReadTunnelRemaining()
        {
            byte[] buffer = new byte[1];
            ReadMemory(buffer, baseAddress, TUNNEL_REMAINING);
            return buffer[0];
        }

        public int ReadLevelIndex()
        {
            byte[] buffer = new byte[1];
            ReadMemory(buffer, baseAddress, LEVEL_IDX);
            return buffer[0];
        }

        public int ReadScore()
        {
            byte[] buffer = new byte[4];
            ReadMemory(buffer, baseAddress, SCORE);
            return BitConverter.ToInt32(buffer, 0);
        }

        public int ReadStageTimeInMilliseconds()
        {
            byte[] buffer = new byte[16];
            ReadMemory(buffer, baseAddress, STAGE_TIME);
            return convertToTime(buffer);
        }

        public int ReadRunTimeInMilliseconds()
        {
            byte[] buffer = new byte[16];
            ReadMemory(buffer, baseAddress, RUN_TIME);
            return convertToTime(buffer);
        }

        private int convertToTime(byte[] buffer)
        {
            int min = BitConverter.ToInt32(buffer, 0);
            int sec = BitConverter.ToInt32(buffer, 4);
            int ms = (int)BitConverter.ToDouble(buffer, 8);

            return 60 * 1000 * min + 1000 * sec + ms;
        }

        public int ReadDamselCount()
        {
            byte[] buffer = new byte[1];
            ReadMemory(buffer, baseAddress, DAMSEL_COUNT);
            return buffer[0];
        }

        public int ReadShoppieCount()
        {
            byte[] buffer = new byte[1];
            ReadMemory(buffer, baseAddress, SHOPPIE_COUNT);
            return buffer[0];
        }

        public int ReadScreenState()
        {
            byte[] buffer = new byte[1];
            ReadMemory(buffer, baseAddress, SCREEN_STATE);
            return buffer[0];
        }

        public int ReadCharSelect()
        {
            byte[] buffer = new byte[1];
            ReadMemory(buffer, baseAddress, CHAR_SELECT);
            return buffer[0];
        }

        public int ReadBombs()
        {
            byte[] buffer = new byte[1];
            ReadMemory(buffer, baseAddress, BOMBS);
            return buffer[0];
        }

        public int ReadPlays()
        {
            byte[] buffer = new byte[4];
            ReadMemory(buffer, baseAddress, PLAYS);
            return BitConverter.ToInt32(buffer, 0);
        }

        public byte[] ReadCharacters()
        {
            byte[] buffer = new byte[16 * 4];
            return ReadMemory(buffer, baseAddress, CHARACTERS);
        }

        public byte[] ReadJournalPlaces()
        {
            byte[] buffer = new byte[10 * 4];
            return ReadMemory(buffer, baseAddress, JOURNAL_PLACES);
        }

        public byte[] ReadJournalMonsters()
        {
            byte[] buffer = new byte[56 * 4];
            return ReadMemory(buffer, baseAddress, JOURNAL_MONSTERS);
        }

        public byte[] ReadJournalItems()
        {
            byte[] buffer = new byte[34 * 4];
            return ReadMemory(buffer, baseAddress, JOURNAL_ITEMS);
        }

        public byte[] ReadJournalTraps()
        {
            byte[] buffer = new byte[14 * 4];
            return ReadMemory(buffer, baseAddress, JOURNAL_TRAPS);
        }

        public int ReadPlayerOneHealthAddr()
        {
            return LocateMemory(baseAddress, PLAYER_ONE_HEALTH);
        }

        public int ReadPlayerTwoHealthAddr()
        {
            return LocateMemory(baseAddress, PLAYER_TWO_HEALTH);
        }

        private byte[] ReadMemory(byte[] buffer, int addr, int[] offsets)
        {
            int bytesRead = 0;

            // Buffer for next pointer
            byte[] pointer = new byte[4];

            // Traverse pointer path
            for (int i = 0; i < offsets.Length - 1; i++)
            {
                addr += offsets[i];
                ReadProcessMemory(processHandle, addr, pointer, pointer.Length, ref bytesRead);
                addr = BitConverter.ToInt32(pointer, 0);
            }

            // Read value from final address
            addr += offsets[offsets.Length - 1];
            ReadProcessMemory(processHandle, addr, buffer, buffer.Length, ref bytesRead);

            return buffer;
        }

        private int LocateMemory(int addr, int[] offsets)
        {
            int bytesRead = 0;

            // Buffer for next pointer
            byte[] pointer = new byte[4];

            // Traverse pointer path
            for (int i = 0; i < offsets.Length - 1; i++)
            {
                addr += offsets[i];
                ReadProcessMemory(processHandle, addr, pointer, pointer.Length, ref bytesRead);
                addr = BitConverter.ToInt32(pointer, 0);
            }

            // Read value from final address
            addr += offsets[offsets.Length - 1];

            return addr;
        }

        public int ReadExactMemory(int addr)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[1];
            ReadProcessMemory(processHandle, addr, buffer, buffer.Length, ref bytesRead);

            return buffer[0];
        }
    }
}