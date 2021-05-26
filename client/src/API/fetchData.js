const fetchData = {

    protocol: "http",
    domain: "localhost",
    port: "443",
    routeToAPI: "/api/v1/",

    get baseURL() {
        return this.protocol + "://" + this.domain + ":" + this.port + this.routeToAPI;
    },

    get domainURL () {
        return this.protocol + "://" + this.domain + ":" + this.port + "/";
    }
};

export default fetchData;