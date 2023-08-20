#region Usings declarations

using System.Text.Json;

using ApprovalTests;
using ApprovalTests.Reporters;

using NFluent;

using Xunit;

#endregion

namespace Reefact.JsonEnumValueBinding.UnitTests {

    [UseReporter(typeof(DiffReporter))]
    public class JsonEnumNameConverter_should {

        #region Statics members declarations

        private static readonly JsonSerializerOptions Options = new() {
            Converters = { new JsonEnumValueConverterFactory() }
        };

        #endregion

        [Fact]
        public void serialize_enum_as_a_property() {
            // Setup
            TrafficLight light = new() {
                Id    = 42,
                Color = TrafficLightColor.Red
            };
            // Exercise
            string json = JsonSerializer.Serialize(light, Options);
            // Verify
            Approvals.Verify(json);
        }

        [Fact]
        public void serialize_enum_as_a_value() {
            // Exercise
            string json = JsonSerializer.Serialize(TrafficLightColor.Green, Options);
            // Verify
            Approvals.Verify(json);
        }

        [Fact]
        public void deserialize_object_containing_enum() {
            // Setup
            string json = "{\"Id\":42,\"Color\":\"rouge\"}";
            // Exercise
            TrafficLight trafficLight = JsonSerializer.Deserialize<TrafficLight>(json, Options);
            // Verify
            Check.That(trafficLight.Color).IsEqualTo(TrafficLightColor.Red);
        }

        [Fact]
        public void deserialize_enum_value_containing_enum() {
            // Setup
            string json = "\"vert\"";
            // Exercise
            TrafficLightColor color = JsonSerializer.Deserialize<TrafficLightColor>(json, Options);
            // Verify
            Check.That(color).IsEqualTo(TrafficLightColor.Green);
        }

        #region Nested types declarations

        private sealed class TrafficLight {

            public long              Id    { get; set; }
            public TrafficLightColor Color { get; set; }

        }

        private enum TrafficLightColor {

            [JsonEnumValue("vert")]   Green,
            [JsonEnumValue("orange")] Orange,
            [JsonEnumValue("rouge")]  Red

        }

        #endregion

    }

}