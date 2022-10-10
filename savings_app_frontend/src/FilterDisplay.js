

const FilterDisplay = ({ filters, setFilters }) => {

    const removeElement = (removableFilter) => {
        setFilters((current) =>
            current.filter((filter) => filter !== removableFilter)
        );
    }

    return (
        <div className=" flex flex-auto gap-1">
            {filters && setFilters && filters.map(filter => {
                return <div className="rounded-full bg-black text-white pl-2 pr-2 flex justify-between gap-2">
                    <p>{filter}</p>
                    <button type="button" onClick={() => removeElement(filter)}>x</button>
                </div>
            })}
        </div>
    );
};

export default FilterDisplay;