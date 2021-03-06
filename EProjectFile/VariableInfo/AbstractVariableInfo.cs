﻿using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace QIQI.EProjectFile
{
    public abstract class AbstractVariableInfo : IHasId, IToTextCodeAble
    {
        public int Id { get; }
        public virtual int[] UBound { get ; set ; }

        public AbstractVariableInfo(int id)
        {
            this.Id = id;
        }
        public int DataType;
        public int Flags;
        /// <summary>
        /// 仅变量、自定义类型成员有效
        /// </summary>
        public string Name;
        public string Comment;
        internal static TElem[] ReadVariables<TElem>(BinaryReader r, Func<int, TElem> newFunction) where TElem : AbstractVariableInfo
        {
            return r.ReadBlocksWithIdAndOffest((reader, id) =>
            {
                var x = newFunction(id);
                x.DataType = reader.ReadInt32();
                x.Flags = reader.ReadInt16();
                x.UBound = reader.ReadInt32sWithFixedLength(reader.ReadByte());
                x.Name = reader.ReadCStyleString();
                x.Comment = reader.ReadCStyleString();
                return x;
            });
        }
        internal static void WriteVariables(BinaryWriter w, AbstractVariableInfo[] variables)
        {
            w.WriteBlocksWithIdAndOffest(variables, (writer, elem) =>
                {
                    writer.Write(elem.DataType);
                    writer.Write((short)elem.Flags);
                    if (elem.UBound == null) 
                    {
                        writer.Write((byte)0);
                    }
                    else
                    {
                        writer.Write((byte)elem.UBound.Length);
                        writer.WriteInt32sWithoutLengthPrefix(elem.UBound);
                    }
                    writer.WriteCStyleString(elem.Name);
                    writer.WriteCStyleString(elem.Comment);
                });
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public abstract void ToTextCode(IdToNameMap nameMap, StringBuilder result, int indent = 0);
    }
}
