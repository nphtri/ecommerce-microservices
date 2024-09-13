import React, { useState } from 'react';

function SearchBox() {
  const [searchValue, setSearchValue] = useState('');
  return (
    <div
      className="search-box"
      onClick={() => {
        document.getElementsByClassName('search-text-holder')[0]!.className += ' display-none';
        document.getElementsByClassName('searchbox-input')[0]!.className += ' searchbox-show';
        (document.getElementsByClassName('searchbox-input')[0]! as HTMLElement).focus();
      }}
    >
      <span className="search-text-holder">Search</span>
      <input
        type="text"
        className="searchbox-input"
        onChange={(e: React.FormEvent<HTMLInputElement>) => {
          setSearchValue(e.currentTarget.value);
        }}
        onBlur={(e: React.FormEvent<HTMLInputElement>) => {
          document.getElementsByClassName('search-text-holder')[0]!.className = 'search-text-holder';
          document.getElementsByClassName('searchbox-input')[0]!.className = 'searchbox-input';
          e.currentTarget.value = '';
        }}
      />
    </div>
  );
}

export default SearchBox;
