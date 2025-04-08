using Core.DataStructures.Art;
using Core.DataStructures.Music;
using Core.Interfaces;
using SketchpadServer.Helpers;
using SketchpadServer.Models.Payloads;

namespace SketchpadServer.Classes
{
    public class CoreAdapter(ICoreMusicProducer coreMusicProducer)
    {
        private readonly ICoreMusicProducer _coreMusicProducer = coreMusicProducer;
        private readonly List<ObjectAttributes> _objectAttributesCache = [];
        private CanvasAttributes? _canvasAttributes;
        private readonly string[] _instruments = ["piano", "violin", "flute", "trumpet"];
        
        public MusicData GenerateMusic(UpdateShapes updateShapes)
        {
            ObjectAttributes objectAttributes = CoreDataMapper.MapToCoreData(ref updateShapes);
            _objectAttributesCache.Add(objectAttributes);

            if (_canvasAttributes == null)
            {
                _canvasAttributes = new()
                {
                    COG = objectAttributes.COG,
                    AreaCovered = objectAttributes.Area
                };
            }
            else
            {
                _canvasAttributes.COG = CalculateGlobalCOG();
                _canvasAttributes.AreaCovered += objectAttributes.Area;
            }

            return _coreMusicProducer.Add(objectAttributes, _canvasAttributes).MusicData;
        }

        public void DeleteShape(string shapeID) 
        {
            if (!_objectAttributesCache.Where(x => x.Id == shapeID).Any())
            {
                return;
            }

            var shapeToDelete = _objectAttributesCache.Where(x => x.Id == shapeID).First();

           if (shapeToDelete is null && _canvasAttributes is null)
           {
                return;
           }

            _objectAttributesCache.Remove(shapeToDelete!);

            if (_objectAttributesCache.Count == 0)
            {
                _canvasAttributes = new() { AreaCovered = 0, COG = (0, 0) };
            }
            else
            {
                double previousCanvasArea = _canvasAttributes!.AreaCovered;
                _canvasAttributes = new()
                {
                    COG = CalculateGlobalCOG(),
                    AreaCovered = previousCanvasArea - shapeToDelete!.Area
                };
            }

            _coreMusicProducer.Remove(shapeToDelete!, _canvasAttributes!);
        }

        public void Clear()
        {
            _canvasAttributes = null;
            _objectAttributesCache.Clear();
            _coreMusicProducer.Clear();
        }

        public string GetInstrument()
        {
            if (_objectAttributesCache.Count == 0)
            {
                return "piano";
            }
            else
            {
                Random rnd = new();
                return _instruments[rnd.NextInt64(0, _instruments.Length - 1)];
            }


        }

        private (double X, double Y) CalculateGlobalCOG()
        {
            return (_objectAttributesCache.Select(x => x.COG.X).Average(),
                _objectAttributesCache.Select(x => x.COG.Y).Average());
        }
    }
}
