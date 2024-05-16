import { Fragment, useState } from "react";

export default function Display(props) {
    const [data, setData] = useState(props.data);

    if (data == null) {
        return (<div>'Loading...'</div>);
    } else {
        return (
            <div style={{ width: "100%" }}>
                {data.length > 0 ? (
                    <div style={{ display: 'grid', gridTemplateColumns: 'repeat(4, 1fr)', gap: '10px' }}>
                        <div style={{ fontWeight: 'bold' }}>Departure</div>
                        <div style={{ fontWeight: 'bold' }}>GateID</div>
                        <div style={{ fontWeight: 'bold' }}>Destination</div>
                        <div style={{ fontWeight: 'bold' }}>Airline</div>
                        {data.map((value, index) => (
                            <Fragment key={index}>
                                <div>{value.Departure}</div>
                                <div>{value.GateID}</div>
                                <div>{value.Destination}</div>
                                <div>{value.Airline}</div>
                            </Fragment>
                        ))}
                    </div>
                ) : (
                    'Loading...'
                )}
            </div>);
    }
}