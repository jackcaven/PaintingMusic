using SketchpadServer.Models.Payloads;

namespace SketchpadServerTests.Mocks
{
    public static class MockUpdateShapes
    {
        public static UpdateShapes CreateValidUpdateShapes()
        {
            return new UpdateShapes
            {
                Sender = "front-end",
                Command = "updateShapes",
                Payload = CreateValidPayload(),
                MessageID = "12345",
                ResponseToMessageID = "67890"
            };
        }

        public static Payload CreateValidPayload()
        {
            return new Payload
            {
                Mode = "Edit",
                Shapes =
                [
                    CreateValidShape("shape1"),
                    CreateValidShape("shape2")
                ]
            };
        }

        public static Shape CreateValidShape(string id)
        {
            return new Shape
            {
                Centre = [0.0, 0.0],
                Sides = 4,
                Area = 16.0,
                Complexity = 1.0,
                Length = 4.0,
                Points =
                [
                    [0.0, 0.0],
                    [0.0, 4.0],
                    [4.0, 4.0],
                    [4.0, 0.0]
                ],
                Color = "#FF0000",
                Id = id
            };
        }

        public static UpdateShapes CreateInvalidUpdateShapes()
        {
            return new UpdateShapes
            {
                Sender = "invalid-sender",
                Command = "unknownCommand",
                Payload = CreateInvalidPayload(),
                MessageID = "",
                ResponseToMessageID = ""
            };
        }

        public static Payload CreateInvalidPayload()
        {
            return new Payload
            {
                Mode = "",
                Shapes = new List<Shape>
                {
                    CreateInvalidShape()
                }
            };
        }

        public static Shape CreateInvalidShape()
        {
            return new Shape
            {
                Centre = [],
                Sides = -1,
                Area = -10.0,
                Complexity = -5.0,
                Length = -1.0,
                Points = [],
                Color = "",
                Id = ""
            };
        }
    }
}
