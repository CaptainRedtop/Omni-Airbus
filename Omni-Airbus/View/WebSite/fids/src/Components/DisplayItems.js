import { Fragment } from "react";
import ColorDiv from "./ColorDiv";
import Divider from "./Divider";

/**
 * DisplayItems component renders a list of departure details in a styled grid layout.
 * Each item in the list includes Departure, GateID, Destination, and Airline information.
 * Items with even-numbered departures are highlighted in yellow using the ColorDiv component.
 * As an alternative one might have placed the color sheme in the c# code 
 * and forwarded it along with the rest of the data.
 * 
 * Props:
 * - data (Array): An array of objects where each object represents a departure with the following properties:
 *   - Departure (string): The departure time.
 *   - GateID (string): The gate number.
 *   - Destination (string): The destination of the flight.
 *   - Airline (string): The airline operating the flight.
 * 
 * @param {Object} props - The component properties.
 * @returns {JSX.Element} The rendered component.
 */
export default function DisplayItems(props) {
    return (
        <div style={{
            background: "black",
            color: "white",
            maxWidth: "750px",
        }}>
            <ColorDiv foreColor="Yellow"><h1>Departures</h1></ColorDiv>
            <Divider color="yellow" />

            <div style={{
                display: 'grid',
                gridTemplateColumns: 'repeat(4, 1fr)',
                gap: '10px',
            }}>
                <ColorDiv foreColor="yellow" >Departure</ColorDiv>
                <ColorDiv foreColor="yellow" >Gate</ColorDiv>
                <ColorDiv foreColor="yellow" >Destination</ColorDiv>
                <ColorDiv foreColor="yellow" >Airline</ColorDiv>
                <Divider color="yellow" />
                <Divider color="yellow" />
                <Divider color="yellow" />
                <Divider color="yellow" />
                {props.data.map((value, index) => (
                    (parseInt(value.Departure.slice(-1)) % 2 == 0) ?
                        <Fragment key={index}>
                            <ColorDiv foreColor="yellow">{value.Departure}</ColorDiv>
                            <ColorDiv foreColor="yellow">{value.GateID}</ColorDiv>
                            <ColorDiv foreColor="yellow">{value.Destination}</ColorDiv>
                            <ColorDiv foreColor="yellow">{value.Airline}</ColorDiv>
                        </Fragment>
                        :
                        <Fragment key={index}>
                            <div>{value.Departure}</div>
                            <div>{value.GateID}</div>
                            <div>{value.Destination}</div>
                            <div>{value.Airline}</div>
                        </Fragment>

                ))}
            </div>
        </div>
    );
}
