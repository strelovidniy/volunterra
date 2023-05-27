function fixChart() {
    let svg = document.getElementsByClassName('mud-chart-line')[0];

    let viewBox = svg.attributes.getNamedItem('viewBox');

    let viewBoxValues = viewBox?.value.split(' ');

    if (Number(viewBoxValues?.[0] ?? 0) < 0) {
        return;
    }

    let newViewBoxValue = `${Number(viewBoxValues?.[0] ?? 0) - 50} ${viewBoxValues?.[1]} ${Number(viewBoxValues?.[2] ?? 0) + 50} ${viewBoxValues?.[3]}`;

    svg.setAttribute('viewBox', newViewBoxValue);
}