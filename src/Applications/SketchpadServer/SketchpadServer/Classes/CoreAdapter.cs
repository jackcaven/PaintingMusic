using Core.DataStructures.Art;
using Core.DataStructures.Music;
using Core.Interfaces;
using SketchpadServer.Helpers;
using SketchpadServer.Models.Payloads;

namespace SketchpadServer.Classes
{
    internal class CoreAdapter(ICoreMusicProducer coreMusicProducer)
    {
        private readonly ICoreMusicProducer _coreMusicProducer = coreMusicProducer;
        private readonly List<ObjectAttributes> _objectAttributesCache = [];
        private CanvasAttributes? _canvasAttributes;
        
        internal MusicData GenerateMusic(UpdateShapes updateShapes)
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

            return _coreMusicProducer.Add(objectAttributes, _canvasAttributes);
        }

        internal void DeleteShape(string shapeID) 
        {
            var shapeToDelete = _objectAttributesCache.Where(x => x.Id == shapeID).First();

           if (shapeToDelete is null && _canvasAttributes is null)
           {
                return;
           }

            _objectAttributesCache.Remove(shapeToDelete!);
            double previousCanvasArea = _canvasAttributes!.AreaCovered;
            _canvasAttributes = new()
            {
                COG = CalculateGlobalCOG(),
                AreaCovered = previousCanvasArea - shapeToDelete!.Area
            };

            _coreMusicProducer.Remove(shapeToDelete!, _canvasAttributes!);
        }

        internal void Clear()
        {
            _canvasAttributes = null;
            _objectAttributesCache.Clear();
            _coreMusicProducer.Clear();
        }

        private (double X, double Y) CalculateGlobalCOG()
        {
            return (_objectAttributesCache.Select(x => x.COG.X).Average(),
                _objectAttributesCache.Select(x => x.COG.Y).Average());
        }
    }
}
