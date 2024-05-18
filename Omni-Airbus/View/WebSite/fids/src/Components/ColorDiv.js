import React from 'react';

/**
 * ColorDiv component renders a div with customizable text and background colors.
 * 
 * Props:
 * - foreColor (string): The text color.
 * - backColor (string): The background color.
 * - children (ReactNode): The content to be rendered inside the div.
 * 
 * 
 * @param {Object} props - The component properties.
 * @returns {JSX.Element} The rendered component.
 */
const ColorDiv = (props) => {
    const { foreColor, backColor } = props;
    const style = {
        color: foreColor,
        backgroundColor: backColor,
    };

    return <div style={style}>{props.children}</div>;
};

export default ColorDiv;
