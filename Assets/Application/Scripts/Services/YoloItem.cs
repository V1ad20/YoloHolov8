using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Barracuda;
using UnityEngine;

namespace YoloHolo.Services
{
    public class YoloItem
    {
        public Vector2 Center { get; }

        public Vector2 Size { get; }

        public Vector2 TopLeft { get; }

        public Vector2 BottomRight { get; }

        public float Confidence { get; }

        public string MostLikelyObject { get; }

        //chnge with model
        public YoloItem(Tensor tensorData, int boxIndex, IYoloClassTranslator translator)
        {
            // var pred = span[(4 + l) * dim + j]; //Basically span[(4 + current_object_class_index) * num_boxes + current_box_index]
            float[] tensorArr = tensorData.ToReadOnlyArray();


            Center = new Vector2(tensorArr[idx(0, boxIndex)], tensorArr[idx(1, boxIndex)]);
            Size = new Vector2(tensorArr[idx(2, boxIndex)], tensorArr[idx(3, boxIndex)]);
            TopLeft = Center - Size / 2;
            BottomRight = Center + Size / 2;

            var classProbabilities = new List<float>();
            for (var i = 4; i < 16; i++) {
                classProbabilities.Add(tensorArr[idx(i,boxIndex)]);
            }
            var maxIndex = classProbabilities.IndexOf(classProbabilities.Max());
            Confidence = classProbabilities[maxIndex];
            MostLikelyObject = translator.GetName(maxIndex);
        }

        //change with model
        private int idx(int box_data_index,int box_index)
        {
            int num_boxes = 1680;
            return box_index * 16 + box_data_index;
        }
    }
}
