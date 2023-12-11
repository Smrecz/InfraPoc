import http from 'k6/http';
import { check } from 'k6';

export let options = {
    stages: [
        { duration: '15s', target: 100 },
        { duration: '5m', target: 100 },
        { duration: '15s', target: 0 },
    ],
};

const payload = JSON.stringify({
    jsonrpc: "2.0",
    method: "eth_getBlockNumber",
    params: [],
    id: 1 
});

const params = {
    headers: {
        'Content-Type': 'application/json',
    },
};

export default function () {
    let response = http.post('http://webapi:8080/rpc', payload, params);

    check(response, {
        'is status 200': (r) => r.status === 200,
    });
}
